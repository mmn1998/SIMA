using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Genders;

public class GenderQueryRepository : IGenderQueryRepository
{
    private readonly string _connectionString;
    public GenderQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetGenderQueryResult> FindById(long id)
    {
        var query = @"SELECT TOP (1000) 
                                 g.[ID] Id
                                ,g.[Name]
                                ,g.[Code]
                                ,a.ID ActiveStatusId
                                ,a.Name ActiveStatus
                            FROM [Basic].[Gender] g
                                join Basic.ActiveStatus a
                                on g.ActiveStatusId = a.ID
                                 WHERE g.ActiveStatusID <> 3 And g.ID = @Id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryFirstOrDefaultAsync<GetGenderQueryResult>(query, new { Id = id });
        result.NullCheck();
        return result;
    }

    public async Task<Result<List<GetGenderQueryResult>>> GetAll(BaseRequest? baseRequest = null)
    {
        var response = new List<GetGenderQueryResult>();
        int totalCount = 0;
        if (baseRequest != null && !string.IsNullOrEmpty(baseRequest.SearchValue))
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
                SELECT DISTINCT 
                   g.[ID] as Id
                  ,g.[Name]
                  ,g.[Code]
                  ,a.ID ActiveStatusId
                  ,a.Name ActiveStatus
,g.[CreatedAt]
              FROM [Basic].[Gender] g
                    join Basic.ActiveStatus a
                    on g.ActiveStatusId = a.ID
                    WHERE (G.Name like @SearchValue OR G.Code like @SearchValue) AND g.[ActiveStatusID] <> 3
Order By g.[CreatedAt] desc
";
                var result = await connection.QueryAsync<GetGenderQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
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
                 SELECT DISTINCT 
                   g.[ID] as Id
                  ,g.[Name]
                  ,g.[Code]
                  ,a.ID ActiveStatusId
                  ,a.Name ActiveStatus
,g.[CreatedAt]
              FROM [Basic].[Gender] g
                    join Basic.ActiveStatus a
                    on g.ActiveStatusId = a.ID
                   WHERE g.[ActiveStatusID] <> 3
Order By g.[CreatedAt] desc
";
                var result = await connection.QueryAsync<GetGenderQueryResult>(query);
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
                 SELECT DISTINCT 
                   g.[ID] as Id
                  ,g.[Name]
                  ,g.[Code]
                  ,a.ID ActiveStatusId
                  ,a.Name ActiveStatus
,g.[CreatedAt]
              FROM [Basic].[Gender] g
                    join Basic.ActiveStatus a
                    on g.ActiveStatusId = a.ID
                    WHERE g.[ActiveStatusID] <> 3
Order By g.[CreatedAt] desc
";
                response = (await connection.QueryAsync<GetGenderQueryResult>(query)).ToList();
            }
        }
        return Result.Ok(response, totalCount);
    }
}
