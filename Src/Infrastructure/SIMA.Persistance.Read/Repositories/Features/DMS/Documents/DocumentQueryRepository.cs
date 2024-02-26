using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.DMS.DocumentExtensions;
using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.Documents;

public class DocumentQueryRepository : IDocumentQueryRepository
{
    private readonly string _connectionString;
    public DocumentQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<List<GetAllDocumentQueryResult>> GetAll(BaseRequest baseRequest)
    {
        var response = new List<GetAllDocumentQueryResult>();
        int totalCount = 0;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(baseRequest.SearchValue))
            {
                string query = @"
SELECT DISTINCT
		D.[Id]
      ,D.[Code]
      ,D.[Name]
	  ,DE.[Name] Extension
,d.[CreatedAt]
  FROM [DMS].[Documents] D
  INNER JOIN [DMS].[DocumentExtension] DE on D.FileExtensionId = DE.Id
  WHERE (DE.Name like @SearchValue OR D.Code like @SearchValue OR D.Name like @SearchValue)
Order By d.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetAllDocumentQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
            }
            else
            {
                string query = @"
SELECT DISTINCT
		D.[Id]
      ,D.[Code]
      ,D.[Name]
	  ,DE.[Name] Extension
,d.[CreatedAt]
  FROM [DMS].[Documents] D
  INNER JOIN [DMS].[DocumentExtension] DE on D.FileExtensionId = DE.Id
Order By d.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetAllDocumentQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
            }
        }
        return response;
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
