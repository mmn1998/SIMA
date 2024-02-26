using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueTypes
{
    public class IssueTypeQueryRepositoty : IIssueTypeQueryRepositoty
    {
        private readonly string _connectionString;
        public IssueTypeQueryRepositoty(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        public async Task<Result<List<GetIssueTypesQueryResult>>> GetAll(BaseRequest baseRequest)
        {
            var response = new List<GetIssueTypesQueryResult>();
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
                      ,P.[ColorHex]
                      ,P.[IconPath]
                      ,P.[ActiveStatusId]
	                  , S.[Name] as ActiveStatus
,p.[CreatedAt]
                  FROM [IssueManagement].[IssueType] P
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                    WHERE (P.[Name]  like @SearchValue or P.[Code] like @SearchValue or S.[Name] like @SearchValue) and P.ActiveStatusId != 3
Order By p.[CreatedAt] desc  ";
                    var result = await connection.QueryAsync<GetIssueTypesQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
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
                      ,P.[ColorHex]
                      ,P.[IconPath]
                      ,P.[ActiveStatusId]
,p.[CreatedAt]
	                  , S.[Name] as ActiveStatus
                  FROM [IssueManagement].[IssueType] P
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                  where P.ActiveStatusId != 3
Order By p.[CreatedAt] desc  ";
                    var result = await connection.QueryAsync<GetIssueTypesQueryResult>(query);
                    response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                    .Take(baseRequest.Take)
                    .ToList();
                }

            }
            return Result.Ok(response, totalCount);
        }

        public async Task<GetIssueTypesQueryResult> GetById(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
              SELECT DISTINCT P.[Id]
                      ,P.[Name]
                      ,P.[Code]
                      ,P.[ColorHex]
                      ,P.[IconPath]
                      ,P.[ActiveStatusId]
	                  , S.[Name] as ActiveStatus
                  FROM [IssueManagement].[IssueType] P
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                  WHERE P.Id = @Id and P.ActiveStatusId != 3";
                var result = await connection.QueryFirstOrDefaultAsync<GetIssueTypesQueryResult>(query, new { Id = id });
                result.NullCheck();
                if (result.ActiveStatusId == 2 || result.ActiveStatusId == 4) throw SimaResultException.IssueTypeDeactiveError;
                if (result.ActiveStatusId == 3) throw SimaResultException.IssueTypeDeleteError;
                return result;
            }
        }
    }
}
