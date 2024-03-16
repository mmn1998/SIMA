using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
  WHERE (@SearchValue is null OR DT.Name like @SearchValue OR DT.Code like @SearchValue OR S.Name like @SearchValue)
 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
            using (var result = await connection.QueryMultipleAsync(query + countQuery, new
            {
                SearchValue = "%" + request.Filter + "%",
                request.Skip,
                request.PageSize,
            }))
            {
                response = (await result.ReadAsync<GetDocumentTypeQueryResult>()).ToList();
                totalCount = await result.ReadSingleAsync<int>();
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
