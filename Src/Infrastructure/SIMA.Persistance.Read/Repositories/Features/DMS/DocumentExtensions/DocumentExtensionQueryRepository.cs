using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;
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
    public async Task<Result<IEnumerable<GetDocumentExtensionQueryResult>>> GetAll(GetAllDocumentExtensionsQuery request)
    {
        int totalCount = 0;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
                string queryCount = @"  WITH Query as(
						  SELECT DISTINCT DE.[Id]
      ,DE.[Name]
      ,DE.[Code]
      ,DE.[ActiveStatusId]
	  ,S.Name ActiveStatus
,de.[CreatedAt]
  FROM [DMS].[DocumentExtension] DE
  INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
  WHERE  DE.[ActiveStatusId] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
                string query = $@" WITH Query as(
							SELECT DISTINCT DE.[Id]
      ,DE.[Name]
      ,DE.[Code]
      ,DE.[ActiveStatusId]
	  ,S.Name ActiveStatus
,de.[CreatedAt]
  FROM [DMS].[DocumentExtension] DE
  INNER JOIN [Basic].[ActiveStatus] S on DE.ActiveStatusId = S.ID
  WHERE DE.[ActiveStatusId] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetDocumentExtensionQueryResult>();
                return Result.Ok(response, request, count);
            }
            
        }
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
