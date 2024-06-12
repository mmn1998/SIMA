using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.DMS.Documents;
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
    public async Task<Result<IEnumerable<GetDocumentTypeQueryResult>>> GetAll(GetAllDocumentTypesQuery request)
    {
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
           
                string queryCount = @" WITH Query as(
						  SELECT DISTINCT DT.[Id]
      ,DT.[Name]
      ,DT.[Code]
      ,DT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,dt.[CreatedAt]
  FROM [DMS].[DocumentType] DT
  INNER JOIN [Basic].[ActiveStatus] S on DT.ActiveStatusId = S.ID
  WHERE DT.[ActiveStatusId] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";

                string query = $@" WITH Query as(
							SELECT DISTINCT DT.[Id]
      ,DT.[Name]
      ,DT.[Code]
      ,DT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,dt.[CreatedAt]
  FROM [DMS].[DocumentType] DT
  INNER JOIN [Basic].[ActiveStatus] S on DT.ActiveStatusId = S.ID
  WHERE DT.[ActiveStatusId] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetDocumentTypeQueryResult>();
                return Result.Ok(response, request, count);
            }
            
        }
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
