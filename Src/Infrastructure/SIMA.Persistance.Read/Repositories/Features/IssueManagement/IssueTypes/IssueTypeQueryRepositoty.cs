using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueTypes;

public class IssueTypeQueryRepositoty : IIssueTypeQueryRepositoty
{
    private readonly string _connectionString;
    public IssueTypeQueryRepositoty(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetIssueTypesQueryResult>>> GetAll(GetAllIssueTypesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @"  WITH Query as(
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
   WHERE  P.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

            var query = $@" WITH Query as(
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
   WHERE  P.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetIssueTypesQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
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
            if (result.ActiveStatusId == 2 || result.ActiveStatusId == 4) throw new SimaResultException("10040", Messages.IssueTypeDeactiveError);
            if (result.ActiveStatusId == 3) throw new SimaResultException("10041", Messages.IssueTypeDeleteError);
            return result;
        }
    }
}
