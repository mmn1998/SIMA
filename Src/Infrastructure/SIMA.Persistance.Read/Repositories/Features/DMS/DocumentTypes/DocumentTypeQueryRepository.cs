using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.DMS.DocumentTypes;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.DocumentTypes;

public class DocumentTypeQueryRepository : IDocumentTypeQueryRepository
{
    private readonly string _connectionString;
    public DocumentTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetDocumentTypeQueryResult>>> GetAll(GetAllDocumentTypesQuery request)
    {
        var response = new List<GetDocumentTypeQueryResult>();
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
                        SELECT DISTINCT DT.[Id]
                              ,DT.[Name]
                              ,DT.[Code]
                              ,DT.[ActiveStatusId]
	                          ,S.Name ActiveStatus
                              ,dt.[CreatedAt]
                          FROM [DMS].[DocumentType] DT
                          INNER JOIN [Basic].[ActiveStatus] S on DT.ActiveStatusId = S.ID
                          WHERE DT.[ActiveStatusId] <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT DT.[Id]
                              ,DT.[Name]
                              ,DT.[Code]
                              ,DT.[ActiveStatusId]
	                          ,S.Name ActiveStatus
                              ,dt.[CreatedAt]
                          FROM [DMS].[DocumentType] DT
                          INNER JOIN [Basic].[ActiveStatus] S on DT.ActiveStatusId = S.ID
                          WHERE DT.[ActiveStatusId] <> 3
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
                    response = (await multi.ReadAsync<GetDocumentTypeQueryResult>()).ToList();
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
            else
            {
                string countQuery = @"SELECT COUNT(*) Result
  FROM [DMS].[DocumentType] DT
  INNER JOIN [Basic].[ActiveStatus] S on DT.ActiveStatusId = S.ID
  WHERE (DT.Name like @SearchValue OR DT.Code like @SearchValue OR S.Name like @SearchValue)";
                string query = $@"
SELECT DISTINCT DT.[Id]
      ,DT.[Name]
      ,DT.[Code]
      ,DT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,dt.[CreatedAt]
  FROM [DMS].[DocumentType] DT
  INNER JOIN [Basic].[ActiveStatus] S on DT.ActiveStatusId = S.ID
  WHERE DT.[ActiveStatusId] <> 3 AND (@SearchValue is null OR DT.Name like @SearchValue OR DT.Code like @SearchValue OR S.Name like @SearchValue)
 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var result = await connection.QueryMultipleAsync(query + countQuery, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize,
                }))
                {
                    response = (await result.ReadAsync<GetDocumentTypeQueryResult>()).ToList();
                    totalCount = await result.ReadSingleAsync<int>();
                }
            }
        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }

    public async Task<GetDocumentTypeQueryResult> GetById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT DT.[Id]
      ,DT.[Name]
      ,DT.[Code]
      ,DT.[ActiveStatusId]
	  ,S.Name ActiveStatus
  FROM [DMS].[DocumentType] DT
  INNER JOIN [Basic].[ActiveStatus] S on DT.ActiveStatusId = S.ID
  WHERE DT.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetDocumentTypeQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }
}
