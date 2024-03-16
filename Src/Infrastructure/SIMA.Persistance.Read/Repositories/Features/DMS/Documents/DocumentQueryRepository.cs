using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.DMS.DocumentExtensions;
using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.Documents;

public class DocumentQueryRepository : IDocumentQueryRepository
{
    private readonly string _connectionString;
    public DocumentQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<List<GetAllDocumentQueryResult>>> GetAll(GetAllDocumentsQuery request)
    {
        var response = new List<GetAllDocumentQueryResult>();
        int totalCount = 0;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string countQuery = @"
SELECT COUNT(*) Result
  FROM [DMS].[Documents] D
  INNER JOIN [DMS].[DocumentExtension] DE on D.FileExtensionId = DE.Id
  WHERE (@SearchValue is null OR DE.Name like @SearchValue OR D.Code like @SearchValue OR D.Name like @SearchValue)
";
            string query = $@"
SELECT DISTINCT
		D.[Id]
      ,D.[Code]
      ,D.[Name]
	  ,DE.[Name] Extension
,d.[CreatedAt] CreatedAt
  FROM [DMS].[Documents] D
  INNER JOIN [DMS].[DocumentExtension] DE on D.FileExtensionId = DE.Id
  WHERE (@SearchValue is null OR DE.Name like @SearchValue OR D.Code like @SearchValue OR D.Name like @SearchValue)
 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"} 
OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
            using (var result = await connection.QueryMultipleAsync(query + countQuery, new 
            {
                SearchValue = "%" + request.Filter + "%",
                 request.PageSize,
                request.Skip,
            }))
            {
                response = (await result.ReadAsync<GetAllDocumentQueryResult>()).ToList();
                totalCount = await result.ReadSingleAsync<int>();
            }

        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }

    public async Task<GetDocumentResult> GetForDownload(long documentId)
    {
        var result = new GetDocumentResult();
        string query = @"
SELECT DISTINCT
		D.[Id]
      ,D.[FileAddress]
	  ,DE.[Name] Extension
  FROM [DMS].[Documents] D
  INNER JOIN [DMS].[DocumentExtension] DE on D.FileExtensionId = DE.Id
  WHERE D.Id = @Id AND D.ActiveStatusId <> 3
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            result = await connection.QueryFirstOrDefaultAsync<GetDocumentResult>(query, new { Id = documentId });
            result.NullCheck();
        }
        return result;
    }
}
