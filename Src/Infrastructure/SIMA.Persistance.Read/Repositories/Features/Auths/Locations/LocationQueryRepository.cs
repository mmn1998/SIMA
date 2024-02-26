using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Locations;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Persistence;

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
            if (result is null) throw SimaResultException.LocationNotFoundError;
            return result;
        }
    }

    public async Task<Result<List<GetLocationQueryResult>>> GetAll(BaseRequest? baseRequest = null)
    {
        int totalCount = 0;
        var response = new List<GetLocationQueryResult>();
        if (baseRequest != null && !string.IsNullOrEmpty(baseRequest.SearchValue))
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
,l.[CreatedAt]
                  FROM [Basic].[Location] L
                       join Basic.ActiveStatus a
                       on L.ActiveStatusId = a.ID
                  INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                    left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
              WHERE (L.Name like @SearchValue OR L.Code like @SearchValue OR LT.Name like @SearchValue OR PLT.Name like @SearchValue) AND L.[ActiveStatusID] <> 3
Order By l.[CreatedAt] desc
";
                var result = await connection.QueryAsync<GetLocationQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                totalCount = result.Count();
                response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
            }
        }
        else if (baseRequest != null && string.IsNullOrEmpty(baseRequest.SearchValue))
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
,l.[CreatedAt]
                  FROM [Basic].[Location] L
                       join Basic.ActiveStatus a
                       on L.ActiveStatusId = a.ID
                  INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                    left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
              WHERE L.[ActiveStatusID] <> 3
Order By l.[CreatedAt] desc
";
                var result = await connection.QueryAsync<GetLocationQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
            }
        }
        else
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
,l.[CreatedAt]
                  FROM [Basic].[Location] L
                       join Basic.ActiveStatus a
                       on L.ActiveStatusId = a.ID
                  INNER JOIN [Basic].[LocationType] LT on L.LocationTypeID = LT.ID
                    left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
              WHERE L.[ActiveStatusID] <> 3
Order By l.[CreatedAt] desc
";
                response = (await connection.QueryAsync<GetLocationQueryResult>(query)).ToList();
            }
        }
        return Result.Ok(response, totalCount);
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
