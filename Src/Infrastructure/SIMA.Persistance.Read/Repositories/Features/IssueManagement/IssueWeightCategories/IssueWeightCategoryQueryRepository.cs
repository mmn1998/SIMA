using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueWeightCategories;

public class IssueWeightCategoryQueryRepository : IIssueWeightCategoryQueryRepository
{
    private readonly string _connectionString;
    public IssueWeightCategoryQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetIssueWeightCategoryQueryResult>>> GetAll(GetAllIssueWeightCategoriesQuery request)
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
                        SELECT WG.[Id]
                                    ,WG.[Name]
                                    ,WG.[Code]
                                    ,WG.[MinRange]
                                    ,WG.[MaxRange]
                                    ,WG.[ActiveStatusId]
                                    ,S.[Name] ActiveStatus
                                    ,wg.[CreatedAt]
                        FROM [IssueManagement].[IssueWeightCategory] WG
                        INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
                        WHERE WG.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT WG.[Id]
                                    ,WG.[Name]
                                    ,WG.[Code]
                                    ,WG.[MinRange]
                                    ,WG.[MaxRange]
                                    ,WG.[ActiveStatusId]
                                    ,S.[Name] ActiveStatus
                                    ,wg.[CreatedAt]
                        FROM [IssueManagement].[IssueWeightCategory] WG
                        INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
                        WHERE WG.ActiveStatusId != 3
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
                    var response = await multi.ReadAsync<GetIssueWeightCategoryQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                var queryCount = @"
                              SELECT  COUNT(*) Result  
                                  FROM [IssueManagement].[IssueWeightCategory] WG
                                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
                                    WHERE (@SearchValue is null OR (WG.[MinRange] like @SearchValue or WG.[MaxRange] like @SearchValue or WG.[Name] like @SearchValue or WG.[Code] like @SearchValue )) and WG.ActiveStatusId != 3";
                var query = $@"
                              SELECT WG.[Id]
                                    ,WG.[Name]
                                    ,WG.[Code]
                                    ,WG.[MinRange]
                                    ,WG.[MaxRange]
                                    ,WG.[ActiveStatusId]
                                    ,S.[Name] ActiveStatus
                                    ,wg.[CreatedAt]
                                FROM [IssueManagement].[IssueWeightCategory] WG
                                INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
                                  WHERE (@SearchValue is null OR (WG.[MinRange] like @SearchValue or WG.[MaxRange] like @SearchValue or WG.[Name] like @SearchValue or WG.[Code] like @SearchValue )) and WG.ActiveStatusId != 3
                               order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetIssueWeightCategoryQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }

    public async Task<GetIssueWeightCategoryQueryResult> GetById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT TOP (1000) WG.[Id]
      ,WG.[Name]
      ,WG.[Code]
      ,WG.[MinRange]
      ,WG.[MaxRange]
      ,WG.[ActiveStatusId]
      ,S.[Name] ActiveStatus
  FROM [IssueManagement].[IssueWeightCategory] WG
  INNER JOIN [Basic].[ActiveStatus] S on S.ID = Wg.ActiveStatusId
  WHERE WG.[Id] = @Id and WG.ActiveStatusId != 3
";
            var result = await connection.QueryFirstOrDefaultAsync<GetIssueWeightCategoryQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }

    public async Task<long> GetIdByWeight(int weight)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                            SELECT TOP 1 Id
                            FROM IssueManagement.IssueWeightCategory
                            WHERE MinRange <= @Weight AND MaxRange >= @Weight";
            var result = await connection.QueryFirstOrDefaultAsync<long>(query, new { Weight = weight });
            result.NullCheck();
            return result;
        }
    }
    public async Task<string> GetByWeight(int weight)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                            SELECT TOP 1 Name
                            FROM IssueManagement.IssueWeightCategory
                            WHERE MinRange <= @Weight AND MaxRange >= @Weight";
            var result = await connection.QueryFirstOrDefaultAsync<string>(query, new { Weight = weight });
            if (result == null) throw (SimaException)SimaResultException.NotFound;
            return result;
        }
    }
}
