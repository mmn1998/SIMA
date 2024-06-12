using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.DMS.DocumentTypes;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentExtensions;

public class WorkFlowDocumentExtensionQueryRepository : IWorkFlowDocumentExtensionQueryRepository
{
    private readonly string _connectionString;
    public WorkFlowDocumentExtensionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetWorkFlowDocumentExtensionQueryResult>>> GetAll(GetAllWorkFlowDocumentExtensionQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @" WITH Query as(
						  SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentExtensionId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
  FROM [DMS].[WorkflowDocumentExtension] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE  WDT.ActiveStatusId<>3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								  ; ";
            string query = $@"WITH Query as(
							SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentExtensionId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
  FROM [DMS].[WorkflowDocumentExtension] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE  WDT.ActiveStatusId<>3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetWorkFlowDocumentExtensionQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetWorkFlowDocumentExtensionQueryResult> GetById(long id)
    {
        string query = @"
SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentExtensionId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
	  ,CASE WHEN WDT.ActiveStatusID = 2 OR  WDT.ActiveStatusID = 4 THEN 1 else 0 END AS IsDeactivated
, CASE WHEN WDT.ActiveStatusID = 3 THEN 1  else 0 END AS IsDeleted
  FROM [DMS].[WorkflowDocumentExtension] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
WHERE WDT.Id = @Id and  and S.ActiveStatusId<>3
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<GetWorkFlowDocumentExtensionQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }

}
