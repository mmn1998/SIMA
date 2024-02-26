using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Positions;

public class PositionQueryRepository : IPositionQueryRepository
{
    private readonly string _connectionString;

    public PositionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<GetPositionQueryResult> FindById(long Id)
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
	              ,D.Name as DepartmentName
	              ,C.Name as CompanyName
              FROM [Organization].Position P
              LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID
              LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
              WHERE P.[ActiveStatusID] <> 3 AND D.[ActiveStatusID] <> 3 AND C.[ActiveStatusID] <> 3 AND P.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetPositionQueryResult>(query, new { Id });
            if (result is null) throw SimaResultException.PositionNotFoundError;
            if (result.IsDeleted) throw SimaResultException.PositionDeleteError;
            if (result.IsDeactivated) throw SimaResultException.PositionDeleteError;
            return result;
        }
    }


    public async Task<Result<List<GetPositionQueryResult>>> GetAll(BaseRequest? baseRequest = null)
    {
        var response = new List<GetPositionQueryResult>();
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
                  ,A.[Name] as ActiveStatus 
	              ,D.Name as DepartmentName
	              ,C.Name as CompanyName
,p.[CreatedAt]
              FROM [Organization].Position P
              LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID AND D.[ActiveStatusID] <> 3
              LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID AND C.[ActiveStatusID] <> 3
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
              WHERE (P.[Name] like @SearchValue OR C.[Name] like @SearchValue OR D.[Name] like @SearchValue OR P.[Code] like @SearchValue) AND P.[ActiveStatusID] <> 3
Order By p.[CreatedAt] desc
";
                var result = await connection.QueryAsync<GetPositionQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
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
	              ,D.Name as DepartmentName
	              ,C.Name as CompanyName
,p.[CreatedAt]
              FROM [Organization].Position P
              LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID
              LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
              WHERE P.[ActiveStatusID] <> 3 AND D.[ActiveStatusID] <> 3 AND C.[ActiveStatusID] <> 3
Order By p.[CreatedAt] desc
";
                var result = await connection.QueryAsync<GetPositionQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
            }
        }
        return Result.Ok(response, totalCount);
    }
}
