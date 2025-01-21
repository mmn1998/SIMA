using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Locations;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
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
                L.Name as Name,
                L.Code  as Code,
                LT.Name as LocationTypeName,
                LT.Id as LocationTypeId,
                PLT.Name as  ParentLocationTypeName
,PLT.Id as ParentLocationTypeId
                ,a.ID ActiveStatusId
                ,PL.Name ParentName
                ,PL.Id ParentId 
                ,a.Name ActiveStatus
                    FROM [Basic].[Location] L
                        join Basic.ActiveStatus a
                        on L.ActiveStatusId = a.ID
                        INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                        left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                        left join [Basic].[Location] PL on PL.Id = L.ParentId
                        WHERE L.[ActiveStatusID] <> 3 AND L.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetLocationQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException(CodeMessges._100060Code, Messages.LocationNotFoundError);
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetLocationQueryResult>>> GetAll(GetAllLocationQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @"
                            WITH Query as(
						    SELECT DISTINCT L.ID as Id,
		                         L.Name as Name,
		                         L.Code  as Code,
		                         LT.Name as LocationTypeName,
                                 LT.Id as LocationTypeId,
		                         PLT.Name as  ParentLocationTypeName
                                 ,PLT.Id as ParentLocationTypeId
		                         ,a.ID ActiveStatusId
		                         ,a.Name ActiveStatus
                                 ,PL.Name ParentName
                                 ,PL.Id ParentId 
                                 ,L.CreatedAt
                            FROM [Basic].[Location] L
                            join Basic.ActiveStatus a
                            on L.ActiveStatusId = a.ID
                            INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                            left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                            left join [Basic].[Location] PL on PL.Id = L.ParentId
                            WHERE  L.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;
                                  ";

            string query = $@"
                            WITH Query as(SELECT DISTINCT L.ID as Id,
							        L.Name as Name,
							        L.Code  as Code,
							        LT.Name as LocationTypeName,
                                    LT.Id as LocationTypeId,
							        PLT.Name as  ParentLocationTypeName
,PLT.Id as ParentLocationTypeId
							        ,a.ID ActiveStatusId
							        ,a.Name ActiveStatus
                                    ,L.CreatedAt
                                    ,PL.Name ParentName
                                    ,PL.Id ParentId 
							FROM [Basic].[Location] L
							join Basic.ActiveStatus a
							on L.ActiveStatusId = a.ID
							INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
							left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                            left join [Basic].[Location] PL on PL.Id = L.ParentId
							WHERE  L.ActiveStatusId != 3)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetLocationQueryResult>();
                return Result.Ok(response, request, count);
            }


        }
    }

    public async Task<Result<IEnumerable<GetLocationQueryResult>>> GetAllCountries()
    {
        var mainQuery = @"
SELECT DISTINCT L.ID as Id,
		L.Name as Name,
		L.Code  as Code,
		LT.Name as LocationTypeName,
        LT.Id as LocationTypeId,
		PLT.Name as  ParentLocationTypeName
        ,PLT.Id as ParentLocationTypeId
		,a.ID ActiveStatusId
		,a.Name ActiveStatus
        ,L.CreatedAt
        ,PL.Name ParentName
        ,PL.Id ParentId 
FROM [Basic].[Location] L
join Basic.ActiveStatus a
on L.ActiveStatusId = a.ID
INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
left join [Basic].[Location] PL on PL.Id = L.ParentId
WHERE  L.ActiveStatusId != 3 and L.ParentId is null
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryAsync<GetLocationQueryResult>(mainQuery);
        return Result.Ok(result);
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
