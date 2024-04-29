using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssuePriorities;

public class IssuePriorityQueryRepository : IIssuePriorityQueryRepository
{
    private readonly string _connectionString;
    public IssuePriorityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetIssuePriorotyQueryResult>>> GetAll(GetAllIssuePriorotiesQuery request)
    {
        

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string queryCount = @"
                              SELECT COUNT(*) Result
                                      FROM [IssueManagement].[IssuePriority] P
                                      INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                                       WHERE (@SearchValue is null OR  (P.[Ordering] like @SearchValue or P.[Name] like @SearchValue or P.[Code] like @SearchValue or S.[Name] like @SearchValue)) and 
                                       P.ActiveStatusId <> 3 ";

            string query = $@"
                              SELECT DISTINCT P.[Id]
                                    ,P.[Name]
                                    ,P.[Code]
                                    ,P.[Ordering]
                                    ,P.[ActiveStatusId]
	                                , S.[Name] as ActiveStatus
                                    ,p.[CreatedAt]
                                FROM [IssueManagement].[IssuePriority] P
                                INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                                WHERE (@SearchValue is null OR  (P.[Ordering] like @SearchValue or P.[Name] like @SearchValue or P.[Code] like @SearchValue or S.[Name] like @SearchValue)) and 
                                           P.ActiveStatusId <> 3  
                                 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}            
                                  OFFSET @Skip rows FETCH NEXT @PageSize rows only ;";
            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = await multi.ReadAsync<GetIssuePriorotyQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
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
