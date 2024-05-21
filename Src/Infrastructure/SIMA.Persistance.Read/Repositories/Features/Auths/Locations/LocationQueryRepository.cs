using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Locations;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Locations;

public class LocationQueryRepository : ILocationQueryRepository
{
    private readonly SIMADBContext _context;
    private readonly IDistributedRedisService _redisService;
    private readonly string _connectionString;

    public LocationQueryRepository(SIMADBContext context, IConfiguration configuration, IDistributedRedisService redisService)
    {
        _context = context;
        _redisService = redisService;
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetLocationQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT L.ID as Id,
                L.Name as LocationName,
                L.Code  as LocationCode,
                LT.Name as LocationTypeName,
                PLT.Name as  ParentLocationTypeName
                ,a.ID ActiveStatusId
                ,a.Name ActiveStatus
                  FROM [Basic].[Location] L
                       join Basic.ActiveStatus a
                       on L.ActiveStatusId = a.ID
                       INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                       left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                       WHERE L.[ActiveStatusID] <> 3 AND L.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetLocationQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException("10060",Messages.LocationNotFoundError);
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetLocationQueryResult>>> GetAll(GetAllLocationQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
            {
                var splitedFilter = request.Filter.Split(":");
                string? SearchValue = splitedFilter[1].Trim().Sanitize();
                string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
                        SELECT DISTINCT L.ID as Id,
		                        L.Name as LocationName,
		                        L.Code  as LocationCode,
		                        LT.Name as LocationTypeName,
		                        PLT.Name as  ParentLocationTypeName
		                        ,a.ID ActiveStatusId
		                        ,a.Name ActiveStatus
                        FROM [Basic].[Location] L
                        join Basic.ActiveStatus a
                        on L.ActiveStatusId = a.ID
                        INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                        left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                        WHERE  L.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT L.ID as Id,
		                        L.Name as LocationName,
		                        L.Code  as LocationCode,
		                        LT.Name as LocationTypeName,
		                        PLT.Name as  ParentLocationTypeName
		                        ,a.ID ActiveStatusId
		                        ,a.Name ActiveStatus
                        FROM [Basic].[Location] L
                        join Basic.ActiveStatus a
                        on L.ActiveStatusId = a.ID
                        INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                        left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                        WHERE  L.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetLocationQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                string queryCount = @"
                            SELECT Count(*) Result
                            FROM [Basic].[Location] L
                            join Basic.ActiveStatus a
                            on L.ActiveStatusId = a.ID
                            INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                            left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                            WHERE  L.ActiveStatusId != 3
                            and (@SearchValue is null OR L.[Name] like @SearchValue or L.[Code] like @SearchValue)
                                  ";

                string query = $@"
                SELECT DISTINCT L.ID as Id,
		                L.Name as LocationName,
		                L.Code  as LocationCode,
		                LT.Name as LocationTypeName,
		                PLT.Name as  ParentLocationTypeName
		                ,a.ID ActiveStatusId
		                ,a.Name ActiveStatus
                FROM [Basic].[Location] L
                join Basic.ActiveStatus a
                on L.ActiveStatusId = a.ID
                INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                WHERE  L.ActiveStatusId != 3
                and (@SearchValue is null OR L.[Name] like @SearchValue or L.[Code] like @SearchValue)
                order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                OFFSET @Skip rows FETCH NEXT @PageSize rows only;
                ";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetLocationQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }

    public async Task<List<GetParentLocationsByLocationTypeIdQueryResult>> GetParentsByChildId(long locationTypeId)
    {
        var locationType = await _context.LocationTypes.FirstOrDefaultAsync(l => l.Id == new LocationTypeId(locationTypeId));
        locationType.NullCheck();
        var parents = await _context.Locations.Where(l => l.LocationTypeId == locationType.ParentId).ToListAsync();
        return parents.Select(i => new GetParentLocationsByLocationTypeIdQueryResult
        {
            Id = i.Id.Value,
            Code = i.Code,
            Name = i.Name
        }).ToList();

    }
}
