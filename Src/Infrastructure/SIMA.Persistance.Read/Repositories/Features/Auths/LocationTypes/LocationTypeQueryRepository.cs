using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes;

public class LocationTypeQueryRepository : ILocationTypeQueryRepository
{
    private readonly string _connectionString;

    public LocationTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetLocationTypeQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT LT.[ID] as Id
                                ,LT.[Name]
                                ,LT.[Code]
                                ,LT.[ActiveStatusID]
                                ,A.[Name] as ActiveStatus
	                            ,PLT.[Name] ParentName
                  FROM [Basic].[LocationType] LT
                  left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                  join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
                  WHERE LT.[ActiveStatusID] <> 3 AND LT.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetLocationTypeQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.LocationTypeNotFoundError;
            return result;
        }
    }

    public async Task<Result<List<GetLocationTypeQueryResult>>> GetAll(BaseRequest? baseRequest = null)
    {
        var response = new List<GetLocationTypeQueryResult>();
        int totalCount = 0;
        if (baseRequest != null && !string.IsNullOrEmpty(baseRequest.SearchValue))
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
                SELECT DISTINCT LT.[ID] as Id
                                ,LT.[Name]
                                ,LT.[Code]
                                ,LT.[ActiveStatusID]
                                ,A.[Name] as ActiveStatus
	                            ,PLT.[Name] ParentName
,lt.[CreatedAt]
                  FROM [Basic].[LocationType] LT
                  left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                  join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
                  WHERE (LT.Name like @SearchValue OR Lt.Code like @SearchValue) AND LT.[ActiveStatusID] <> 3
Order By lt.[CreatedAt] desc
";
                var result = await connection.QueryAsync<GetLocationTypeQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
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
                SELECT DISTINCT LT.[ID] as Id
                                ,LT.[Name]
                                ,LT.[Code]
                                ,LT.[ActiveStatusID]
                                ,A.[Name] as ActiveStatus
	                            ,PLT.[Name] ParentName
,lt.[CreatedAt]
                  FROM [Basic].[LocationType] LT
                  join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
                  left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                  WHERE LT.[ActiveStatusID] <> 3
Order By lt.[CreatedAt] desc";
                var result = await connection.QueryAsync<GetLocationTypeQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
            }
        }
        else
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
                SELECT DISTINCT LT.[ID] as Id
                                ,LT.[Name]
                                ,LT.[Code]
                                ,LT.[ActiveStatusID]
                                ,A.[Name] as ActiveStatus
	                            ,PLT.[Name] ParentName
,lt.[CreatedAt]
                  FROM [Basic].[LocationType] LT
                  join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
                  left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                  WHERE LT.[ActiveStatusID] <> 3
Order By lt.[CreatedAt] desc
";
                response = (await connection.QueryAsync<GetLocationTypeQueryResult>(query)).ToList();
            }
        }
        return Result.Ok(response, totalCount);
    }
}
