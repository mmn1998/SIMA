using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentExtensions
{
    public class WorkFlowDocumentExtensionQueryRepository : IWorkFlowDocumentExtensionQueryRepository
    {
        private readonly string _connectionString;
        public WorkFlowDocumentExtensionQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }
        public async Task<List<GetWorkFlowDocumentExtensionQueryResult>> GetAll(BaseRequest baseRequest)
        {
            try
            {
                var response = new List<GetWorkFlowDocumentExtensionQueryResult>();
                int totalCount = 0;
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    if (!string.IsNullOrEmpty(baseRequest.SearchValue))
                    {
                        string query = @"
SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentExtensionId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
	  ,CASE WHEN WDT.ActiveStatusID = 2 OR  WDT.ActiveStatusID = 4 THEN 1 else 0 END AS IsDeactivated
, CASE WHEN WDT.ActiveStatusID = 3 THEN 1  else 0 END AS IsDeleted
  FROM [DMS].[WorkflowDocumentExtension] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE (WDT.[WorkflowId] like @SearchValue OR WDT.[DocumentExtensionId] like @SearchValue OR S.Name like @SearchValue)  and S.ActiveStatusId<>3
Order By wdt.[CreatedAt] desc  
";
                        var result = await connection.QueryAsync<GetWorkFlowDocumentExtensionQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                        totalCount = result.Count();
                        response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
                    }
                    else
                    {
                        string query = @"
SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentExtensionId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
	  ,CASE WHEN WDT.ActiveStatusID = 2 OR  WDT.ActiveStatusID = 4 THEN 1 else 0 END AS IsDeactivated
, CASE WHEN WDT.ActiveStatusID = 3 THEN 1  else 0 END AS IsDeleted
  FROM [DMS].[WorkflowDocumentExtension] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
where  wdt.ActiveStatusId<>3
Order By wdt.[CreatedAt] desc  
";
                        var result = await connection.QueryAsync<GetWorkFlowDocumentExtensionQueryResult>(query);
                        totalCount = result.Count();
                        response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
                    }
                    return response;
                }
            }
            catch (Exception e)
            {

                throw;
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
}
