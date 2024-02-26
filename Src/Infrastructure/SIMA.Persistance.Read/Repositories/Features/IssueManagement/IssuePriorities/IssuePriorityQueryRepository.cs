using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssuePriorities;

public class IssuePriorityQueryRepository : IIssuePriorityQueryRepository
{
    private readonly string _connectionString;
    public IssuePriorityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetIssuePriorotyQueryResult>>> GetAll(BaseRequest baseRequest)
    {
        var response = new List<GetIssuePriorotyQueryResult>();
        int totalCount = 0;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = string.Empty;
            if (!string.IsNullOrEmpty(baseRequest.SearchValue))
            {
                query = @"
                SELECT DISTINCT P.[Id]
                      ,P.[Name]
                      ,P.[Code]
                      ,P.[Ordering]
                      ,P.[ActiveStatusId]
	                  , S.[Name] as ActiveStatus
,p.[CreatedAt]
                  FROM [IssueManagement].[IssuePriority] P
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                    WHERE (P.[Ordering] like '@SearchValue' or P.[Name] like N'@SearchValue' or P.[Code] like N'@SearchValue' or S.[Name] like N'@SearchValue')
Order By p.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetIssuePriorotyQueryResult>(query, new { baseRequest.SearchValue });
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                .Take(baseRequest.Take)
                .ToList();
            }
            else
            {
                query = @"
                SELECT DISTINCT P.[Id]
                      ,P.[Name]
                      ,P.[Code]
                      ,P.[Ordering]
                      ,P.[ActiveStatusId]
	                  , S.[Name] as ActiveStatus
,p.[CreatedAt]
                  FROM [IssueManagement].[IssuePriority] P
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId 
                  where P.ActiveStatusId != 3
Order By p.[CreatedAt] desc  ";
                var result = await connection.QueryAsync<GetIssuePriorotyQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                .Take(baseRequest.Take)
                .ToList();
            }

        }
        return Result.Ok(response, totalCount); ;
    }

    public async Task<GetIssuePriorotyQueryResult> GetById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                SELECT P.[Id]
                      ,P.[Name]
                      ,P.[Code]
                      ,P.[Ordering]
                      ,P.[ActiveStatusId]
	                  , S.[Name] as ActiveStatus
                  FROM [IssueManagement].[IssuePriority] P
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                  WHERE P.Id = @Id and P.ActiveStatusId != 3";
            var result = await connection.QueryFirstOrDefaultAsync<GetIssuePriorotyQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }
}
