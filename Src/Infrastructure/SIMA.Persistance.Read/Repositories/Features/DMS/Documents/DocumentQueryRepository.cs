using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
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

    public async Task<Result<IEnumerable<GetAllDocumentQueryResult>>> GetAll(GetAllDocumentsQuery request)
    {
        int totalCount = 0;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @" WITH Query as(
						   SELECT DISTINCT
		D.[Id]
      ,D.[Code]
      ,D.[Name]
	  ,DE.[Name] Extension
,d.[CreatedAt] CreatedAt
  FROM [DMS].[Documents] D
  INNER JOIN [DMS].[DocumentExtension] DE on D.FileExtensionId = DE.Id
  WHERE DE.ActiveStatusId <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 ; ";
            string query = $@" WITH Query as(
							 SELECT DISTINCT
		D.[Id]
      ,D.[Code]
      ,D.[Name]
	  ,DE.[Name] Extension
,d.[CreatedAt] CreatedAt
  FROM [DMS].[Documents] D
  INNER JOIN [DMS].[DocumentExtension] DE on D.FileExtensionId = DE.Id
  WHERE DE.ActiveStatusId <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetAllDocumentQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetDocumentResult> GetForDownload(long documentId, bool? forIssueDetail = false)
    {
        var result = new GetDocumentResult();
        string query = @"
SELECT DISTINCT
		D.[Id]
      ,D.[FileAddress]
	  ,DE.[Name] Extension
,D.[Name] as  Name
  FROM [DMS].[Documents] D
  INNER JOIN [DMS].[DocumentExtension] DE on D.FileExtensionId = DE.Id
  WHERE D.Id = @Id AND D.ActiveStatusId <> 3
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            result = await connection.QueryFirstOrDefaultAsync<GetDocumentResult>(query, new { Id = documentId });
            if (forIssueDetail == false) result.NullCheck();
        }
        return result;
    }
}
