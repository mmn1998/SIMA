using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Framework.Common.Helper;
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
        var query = @"SELECT  
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

    public async Task<Result<IEnumerable<GetGenderQueryResult>>> GetAll(GetAllGenderQuery? request = null)
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
                        SELECT DISTINCT 
                                    g.[ID] as Id
                                   ,g.[Name]
                                   ,g.[Code]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                                   ,g.[CreatedAt]
                            FROM [Basic].[Gender] g
                            join Basic.ActiveStatus a on g.ActiveStatusId = a.ID
                        WHERE g.[ActiveStatusID] <> 3
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT 
                                    g.[ID] as Id
                                   ,g.[Name]
                                   ,g.[Code]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                                   ,g.[CreatedAt]
                            FROM [Basic].[Gender] g
                            join Basic.ActiveStatus a on g.ActiveStatusId = a.ID
                        WHERE g.[ActiveStatusID] <> 3
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
                    var response = await multi.ReadAsync<GetGenderQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                var queryCount = @"
                              SELECT  COUNT(*) Result
                               FROM [Basic].[Gender] g
                               join Basic.ActiveStatus a on g.ActiveStatusId = a.ID
                               WHERE (@SearchValue is null OR(G.Name like @SearchValue OR G.Code like @SearchValue)) AND g.[ActiveStatusID] <> 3";

                string query = $@"
                                 SELECT DISTINCT 
                                    g.[ID] as Id
                                   ,g.[Name]
                                   ,g.[Code]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                                  ,g.[CreatedAt]
                               FROM [Basic].[Gender] g
                                     join Basic.ActiveStatus a on g.ActiveStatusId = a.ID
                                     WHERE (@SearchValue is null OR(G.Name like @SearchValue OR G.Code like @SearchValue)) AND g.[ActiveStatusID] <> 3
                             order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                              OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetGenderQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }
}
