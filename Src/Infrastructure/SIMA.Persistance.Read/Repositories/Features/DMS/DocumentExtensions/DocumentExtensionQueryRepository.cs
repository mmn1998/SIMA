using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.DMS.DocumentExtensions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.DocumentExtensions;

public class DocumentExtensionQueryRepository : IDocumentExtensionQueryRepository
{
    private readonly string _connectionString;
    public DocumentExtensionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetDocumentExtensionQueryResult>>> GetAll(GetAllDocumentExtensionsQuery request)
    {
        var response = new List<GetDocumentExtensionQueryResult>();
        int totalCount = 0;
        
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
                        SELECT DISTINCT DE.[Id]
                              ,DE.[Name]
                              ,DE.[Code]
                              ,DE.[ActiveStatusId]
	                          ,S.Name ActiveStatus
                              ,de.[CreatedAt]
                          FROM [DMS].[DocumentExtension] DE
                          INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
                          WHERE DE.[ActiveStatusId] <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT DE.[Id]
                              ,DE.[Name]
                              ,DE.[Code]
                              ,DE.[ActiveStatusId]
	                          ,S.Name ActiveStatus
                              ,de.[CreatedAt]
                          FROM [DMS].[DocumentExtension] DE
                          INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
                          WHERE DE.[ActiveStatusId] <> 3
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
                    response = (await multi.ReadAsync<GetDocumentExtensionQueryResult>()).ToList();
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
            else
            {
                string countQuery = @"
SELECT Count(*) Result
  FROM [DMS].[DocumentExtension] DE
  INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
  WHERE (@SearchValue is null OR DE.Name like @SearchValue OR DE.Code like @SearchValue OR S.Name like @SearchValue) AND DE.[ActiveStatusId] <> 3
";
                string query = $@"
SELECT DISTINCT DE.[Id]
      ,DE.[Name]
      ,DE.[Code]
      ,DE.[ActiveStatusId]
	  ,S.Name ActiveStatus
,de.[CreatedAt]
  FROM [DMS].[DocumentExtension] DE
  INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
  WHERE (@SearchValue is null OR DE.Name like @SearchValue OR DE.Code like @SearchValue OR S.Name like @SearchValue) AND DE.[ActiveStatusId] <> 3
 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
OFFSET @Skip rows FETCH NEXT @Take rows only;
";
                using (var result = await connection.QueryMultipleAsync(query + countQuery, new
                {
                    request.Skip,
                    Take = request.PageSize,
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%"
                }))
                {
                    response = (await result.ReadAsync<GetDocumentExtensionQueryResult>()).ToList();
                    totalCount = await result.ReadSingleAsync<int>();
                }
            }
        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }

    public async Task<GetDocumentExtensionQueryResult> GetById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT DE.[Id]
      ,DE.[Name]
      ,DE.[Code]
      ,DE.[ActiveStatusId]
	  ,S.Name ActiveStatus
  FROM [DMS].[DocumentExtension] DE
  INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
  WHERE DE.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetDocumentExtensionQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }
}
