using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.DMS.DocumentExtensions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.DocumentExtensions;

public class DocumentExtensionQueryRepository : IDocumentExtensionQueryRepository
{
    private readonly string _connectionString;
    public DocumentExtensionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetDocumentExtensionQueryResult>>> GetAll(BaseRequest baseRequest)
    {
        var response = new List<GetDocumentExtensionQueryResult>();
        int totalCount = 0;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(baseRequest.SearchValue))
            {
                string query = @"
SELECT DISTINCT DE.[Id]
      ,DE.[Name]
      ,DE.[Code]
      ,DE.[ActiveStatusId]
	  ,S.Name ActiveStatus
,de.[CreatedAt]
  FROM [DMS].[DocumentExtension] DE
  INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
  WHERE (DE.Name like @SearchValue OR DE.Code like @SearchValue OR S.Name like @SearchValue)
Order By de.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetDocumentExtensionQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
            }
            else
            {
                string query = @"
SELECT DISTINCT DE.[Id]
      ,DE.[Name]
      ,DE.[Code]
      ,DE.[ActiveStatusId]
	  ,S.Name ActiveStatus
,de.[CreatedAt]
  FROM [DMS].[DocumentExtension] DE
  INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
Order By de.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetDocumentExtensionQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
            }
        }
        return Result.Ok(response, totalCount); ;
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
