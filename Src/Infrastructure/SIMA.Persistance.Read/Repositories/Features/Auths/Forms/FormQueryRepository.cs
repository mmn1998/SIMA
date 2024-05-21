using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Forms;

public class FormQueryRepository : IFormQueryRepository
{
    private readonly string _connectionString;
    public FormQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<List<GetViewResult>> FetchFromView(string viewName)
    {
        string query = @$"SELECT Value,Lable FROM [{viewName}]";
        using (var connection = new SqlConnection(_connectionString))
        {
            var result = (await connection.QueryAsync<GetViewResult>(query)).ToList();
            return result;
        }
    }

    public async Task<GetFormQueryResult> FindById(long id)
    {
        var query = @"Select 
                             F.[Id]
                            ,F.[Name]
                            ,F.[Title]
                            ,F.[Code]
                            ,F.[IsSystemForm]
                            ,F.[JsonContent]
                            ,F.[ActiveStatusId]
                            ,A.[Name] ActiveStatusName
                            From Authentication.Form F
                            join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                            Where F.ActiveStatusId <> 3 and F.Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryFirstOrDefaultAsync<GetFormQueryResult>(query, new { Id = id });
        result.NullCheck();
        return result;
    }

    public async Task<Result<IEnumerable<GetFormQueryResult>>> GetAll(GetAllFormQuery? request = null)
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
                        SELECT 
                             F.[Id]
                            ,F.[Name]
                            ,F.[Title]
                            ,F.[Code]
                            ,F.[IsSystemForm]
                            ,F.[JsonContent]
                            ,F.[ActiveStatusId]
                            ,A.[Name] ActiveStatusName
                            From Authentication.Form F
                            join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                        WHERE F.[ActiveStatusID] <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT 
                             F.[Id]
                            ,F.[Name]
                            ,F.[Title]
                            ,F.[Code]
                            ,F.[IsSystemForm]
                            ,F.[JsonContent]
                            ,F.[ActiveStatusId]
                            ,A.[Name] ActiveStatusName
                            From Authentication.Form F
                            join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                        WHERE F.[ActiveStatusID] <> 3
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
                    var response = await multi.ReadAsync<GetFormQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                var queryCount = @"
                              SELECT  COUNT(*) Result
                               From Authentication.Form F
                               join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                               WHERE (@SearchValue is null OR(F.Name like @SearchValue OR F.Code like @SearchValue)) AND F.[ActiveStatusID] <> 3";

                string query = $@"
                                 Select 
                             F.[Id]
                            ,F.[Name]
                            ,F.[Title]
                            ,F.[Code]
                            ,F.[IsSystemForm]
                            ,F.[JsonContent]
                            ,F.[ActiveStatusId]
                            ,A.[Name] ActiveStatusName
                            From Authentication.Form F
                            join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
                            WHERE (@SearchValue is null OR(F.Name like @SearchValue OR F.Code like @SearchValue)) AND F.[ActiveStatusID] <> 3
                             order by {request.Sort?.Replace(":", " ") ?? "F.CreatedAt desc"}
                              OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetFormQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }

    public async Task<Result<IEnumerable<GetFormFieldsQueryResult>>> GetAllFormFields(GetAllFormFieldsQuery request)
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
                        SELECT FF.[Id]
                          ,FF.[FormId]
                          ,FF.[Name]
                          ,FF.[Code]
                          ,FF.[Type]
                          ,FF.[ActiveStatusId]
	                      ,F.[Name] FormName
	                      ,A.[Name] ActiveStatus
                      FROM [Authentication].[FormField] FF
                      INNER JOIN [Authentication].[Form] F ON F.Id = FF.FormId AND F.ActiveStatusId <> 3
                      INNER JOIN [Basic].[ActiveStatus] A ON A.ID = FF.ActiveStatusId
                      WHERE FF.ActiveStatusId <> 3 AND (@FormId is null or F.Id = @FormId)
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT FF.[Id]
                              ,FF.[FormId]
                              ,FF.[Name]
                              ,FF.[Code]
                              ,FF.[Type]
                              ,FF.[ActiveStatusId]
	                          ,F.[Name] FormName
	                          ,A.[Name] ActiveStatus
                          FROM [Authentication].[FormField] FF
                          INNER JOIN [Authentication].[Form] F ON F.Id = FF.FormId AND F.ActiveStatusId <> 3
                          INNER JOIN [Basic].[ActiveStatus] A ON A.ID = FF.ActiveStatusId
                          WHERE FF.ActiveStatusId <> 3 AND (@FormId is null or F.Id = @FormId)
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    request.Skip,
                    request.PageSize,
                    request.FormId
                }))
                {
                    var response = await multi.ReadAsync<GetFormFieldsQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                var queryCount = @"
                              SELECT  COUNT(*) Result
                               FROM [Authentication].[FormField] FF
                                  INNER JOIN [Authentication].[Form] F ON F.Id = FF.FormId AND F.ActiveStatusId <> 3
                                  INNER JOIN [Basic].[ActiveStatus] A ON A.ID = FF.ActiveStatusId
                                  WHERE FF.ActiveStatusId <> 3 AND (@FormId is null or F.Id = @FormId)
                                    AND (@SearchValue is null OR(F.Name like @SearchValue OR FF.Code like @SearchValue OR FF.Name like @SearchValue OR FF.Type like @SearchValue OR A.Name like @SearchValue))";

                string query = $@"
                                 SELECT FF.[Id]
                                      ,FF.[FormId]
                                      ,FF.[Name]
                                      ,FF.[Code]
                                      ,FF.[Type]
                                      ,FF.[ActiveStatusId]
	                                  ,F.[Name] FormName
	                                  ,A.[Name] ActiveStatus
                                  FROM [Authentication].[FormField] FF
                                  INNER JOIN [Authentication].[Form] F ON F.Id = FF.FormId AND F.ActiveStatusId <> 3
                                  INNER JOIN [Basic].[ActiveStatus] A ON A.ID = FF.ActiveStatusId
                                  WHERE FF.ActiveStatusId <> 3 AND (@FormId is null or F.Id = @FormId)
                                    AND (@SearchValue is null OR(F.Name like @SearchValue OR FF.Code like @SearchValue OR FF.Name like @SearchValue OR FF.Type like @SearchValue OR A.Name like @SearchValue))
                             order by {request.Sort?.Replace(":", " ") ?? "F.CreatedAt desc"}
                              OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize,
                    request.FormId
                }))
                {
                    var response = await multi.ReadAsync<GetFormFieldsQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }
}

