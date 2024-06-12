using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
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
           
                string queryCount = @"   WITH Query as(
						   SELECT DISTINCT P.[Id]
       ,P.[Name]
       ,P.[Code]
       ,P.[Ordering]
       ,P.[ActiveStatusId]
       , S.[Name] as ActiveStatus
       ,p.[CreatedAt]
   FROM [IssueManagement].[IssuePriority] P
   INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
   WHERE P.ActiveStatusId <> 3  
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                string query = $@" WITH Query as(
							SELECT DISTINCT P.[Id]
       ,P.[Name]
       ,P.[Code]
       ,P.[Ordering]
       ,P.[ActiveStatusId]
       , S.[Name] as ActiveStatus
       ,p.[CreatedAt]
   FROM [IssueManagement].[IssuePriority] P
   INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
   WHERE P.ActiveStatusId <> 3  
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetIssuePriorotyQueryResult>();
                return Result.Ok(response, request, count);
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
