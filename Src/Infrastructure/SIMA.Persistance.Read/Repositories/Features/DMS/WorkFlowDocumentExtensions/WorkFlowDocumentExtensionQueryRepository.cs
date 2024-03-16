using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
    public async Task<Result<List<GetWorkFlowDocumentExtensionQueryResult>>> GetAll(GetAllWorkFlowDocumentExtensionQuery request)
    {
        var response = new List<GetWorkFlowDocumentExtensionQueryResult>();
        int totalCount = 0;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string countQuery = @"SELECT COUNT(*) Result
  FROM [DMS].[WorkflowDocumentExtension] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE (WDT.[WorkflowId] like @SearchValue OR WDT.[DocumentExtensionId] like @SearchValue OR S.Name like @SearchValue)  and WDT.ActiveStatusId<>3";
            string query = $@"
SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentExtensionId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
  FROM [DMS].[WorkflowDocumentExtension] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE (@SearchValue is null OR WDT.[WorkflowId] like @SearchValue OR WDT.[DocumentExtensionId] like @SearchValue OR S.Name like @SearchValue)  and WDT.ActiveStatusId<>3
 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
            using (var result = await connection.QueryMultipleAsync(query + countQuery, new
            {
                SearchValue = "%" + request.Filter + "%",
                request.PageSize,
                request.Skip
            }))
            {
                response = (await result.ReadAsync<GetWorkFlowDocumentExtensionQueryResult>()).ToList();
                totalCount = await result.ReadSingleAsync<int>();
            }

            return Result.Ok(response, totalCount, request.PageSize, request.Page);
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
