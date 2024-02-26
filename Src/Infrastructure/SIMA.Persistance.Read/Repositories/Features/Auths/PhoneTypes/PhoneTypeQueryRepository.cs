using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.PhoneTypes;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.PhoneTypes;

public class PhoneTypeQueryRepository : IPhoneTypeQueryRepository
{
    private readonly string _connectionString;

    public PhoneTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetPhoneTypeQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT P.[ID] as Id
                  ,P.[Name]
                  ,P.[Code]
                  ,P.[ActiveStatusID]
                  ,A.[Name] as ActiveStatus 
              FROM [Basic].[PhonType] P
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
              WHERE [ActiveStatusID] <> 3 AND P.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetPhoneTypeQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }

    public async Task<Result<List<GetPhoneTypeQueryResult>>> GetAll(BaseRequest? baseRequest = null)
    {

        var response = new List<GetPhoneTypeQueryResult>();
        int totalCount = 0;
        if (baseRequest != null && !string.IsNullOrEmpty(baseRequest.SearchValue))
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
                SELECT DISTINCT P.[ID] as Id
                  ,P.[Name]
                  ,P.[Code]
                  ,P.[ActiveStatusID]
,p.[CreatedAt]
                  ,A.[Name] as ActiveStatus 
              FROM [Basic].[PhonType] P
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
              WHERE (P.Name like @SearchValue OR P.Code like @SearchValue) AND P.[ActiveStatusID] <> 3
Order By p.[CreatedAt] desc";
                var result = await connection.QueryAsync<GetPhoneTypeQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
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
                SELECT DISTINCT P.[ID] as Id
                  ,P.[Name]
                  ,P.[Code]
                  ,P.[ActiveStatusID]
                  ,A.[Name] as ActiveStatus 
,p.[CreatedAt]
              FROM [Basic].[PhonType] P
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
              WHERE P.[ActiveStatusID] <> 3
Order By p.[CreatedAt] desc
";
                var result = await connection.QueryAsync<GetPhoneTypeQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
            }
        }
        return Result.Ok(response, totalCount);
    }
}
