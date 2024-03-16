using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
    public async Task<Result<List<GetWorkFlowDocumentTypeQueryResult>>> GetAll(GetAllWorkFlowDocumentTypesQuery request)
    {
        var response = new List<GetWorkFlowDocumentTypeQueryResult>();
        int totalCount = 0;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string countQuery = @"SELECT DISTINCT Count(*) Result
  FROM [DMS].[WorkflowDocumentType] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE (@SearchValue is null OR (WDT.[WorkflowId] like @SearchValue OR WDT.[DocumentTypeId] like @SearchValue OR S.Name like @SearchValue))";
            string query = $@"
SELECT DISTINCT WDT.[Id]
      ,WDT.[WorkflowId]
      ,WDT.[DocumentTypeId]
      ,WDT.[ActiveStatusId]
	  ,S.Name ActiveStatus
,wdt.[CreatedAt]
  FROM [DMS].[WorkflowDocumentType] WDT
  INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
 WHERE (@SearchValue is null OR ( WDT.[WorkflowId] like @SearchValue OR WDT.[DocumentTypeId] like @SearchValue OR S.Name like @SearchValue))
 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
OFFSET @Skip rows FETCH NEXT @Take rows only;
";
            using (var result = await connection.QueryMultipleAsync(query + countQuery, new
            {
                SearchValue = "%" + request.Filter + "%",
                Take = request.PageSize,
                request.Skip
            }))
            {
                response = (await result.ReadAsync<GetWorkFlowDocumentTypeQueryResult>()).ToList();
                totalCount = await result.ReadSingleAsync<int>();
            }
            return Result.Ok(response, totalCount, request.PageSize, request.Page);
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
