using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;
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
            if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
            {
                var splitedFilter = request.Filter.Split(":");
                string? SearchValue = splitedFilter[1].Trim().Sanitize();
                string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
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
                        WHERE P.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
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
                        WHERE P.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetIssueTypesQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                var queryCount = @"
                              SELECT  COUNT(*) Result 
                                   FROM [IssueManagement].[IssueType] P
                                   INNER JOIN [Basic].[ActiveStatus] S on S.ID = P.ActiveStatusId
                                     WHERE (@SearchValue is null OR (P.[Name]  like @SearchValue or P.[Code] like @SearchValue or S.[Name] like @SearchValue)) and P.ActiveStatusId != 3";

                var query = $@"
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
                                    WHERE (@SearchValue is null OR (P.[Name]  like @SearchValue or P.[Code] like @SearchValue or S.[Name] like @SearchValue)) and P.ActiveStatusId != 3
                                    order by  {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetIssueTypesQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
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
            if (result.ActiveStatusId == 2 || result.ActiveStatusId == 4) throw new SimaResultException("10040",Messages.IssueTypeDeactiveError);
            if (result.ActiveStatusId == 3) throw new SimaResultException("10041",Messages.IssueTypeDeleteError);
            return result;
        }
    }
}
