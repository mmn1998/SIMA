using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentTypes;

public class WorkFlowDocumentTypeQueryRepository : IWorkFlowDocumentTypeQueryRepository
{
    private readonly string _connectionString;
    public WorkFlowDocumentTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetWorkFlowDocumentTypeQueryResult>>> GetAll(GetAllWorkFlowDocumentTypesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
                string queryCount = @" WITH Query as(
						  SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentTypeId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
  FROM [DMS].[WorkflowDocumentType] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE WDT.ActiveStatusId <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
                string query = $@" WITH Query as(
							SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentTypeId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
  FROM [DMS].[WorkflowDocumentType] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE WDT.ActiveStatusId <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetWorkFlowDocumentTypeQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetWorkFlowDocumentTypeQueryResult> GetById(long id)
    {
        string query = @"
SELECT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentTypeId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
  FROM [DMS].[WorkflowDocumentType] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
WHERE WDT.Id = @Id
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<GetWorkFlowDocumentTypeQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }
}
