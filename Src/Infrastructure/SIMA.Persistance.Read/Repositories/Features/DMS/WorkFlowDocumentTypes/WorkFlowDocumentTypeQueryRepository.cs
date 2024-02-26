using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentTypes;

public class WorkFlowDocumentTypeQueryRepository : IWorkFlowDocumentTypeQueryRepository
{
    private readonly string _connectionString;
    public WorkFlowDocumentTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<List<GetWorkFlowDocumentTypeQueryResult>>> GetAll(BaseRequest baseRequest)
    {
        var response = new List<GetWorkFlowDocumentTypeQueryResult>();
        int totalCount = 0;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(baseRequest.SearchValue))
            {
                string query = @"
SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentTypeId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
  FROM [DMS].[WorkflowDocumentType] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE (WDT.[WorkflowId] like @SearchValue OR WDT.[DocumentTypeId] like @SearchValue OR S.Name like @SearchValue)
Order By wdt.[CreatedAt] desc  

";
                var result = await connection.QueryAsync<GetWorkFlowDocumentTypeQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
            }
            else
            {
                string query = @"
SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentTypeId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
  FROM [DMS].[WorkflowDocumentType] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
Order By wdt.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetWorkFlowDocumentTypeQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take).Take(baseRequest.Take).ToList();
            }
            return Result.Ok(response, totalCount);
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
