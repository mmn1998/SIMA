using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
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
            if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
            {
                var splitedFilter = request.Filter.Split(":");
                string? SearchValue = splitedFilter[1].Trim().Sanitize();
                string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
                        SELECT DISTINCT WDT.[Id]
                              ,WDT.[WorkflowId]
                              ,WDT.[DocumentTypeId]
                              ,WDT.[ActiveStatusId]
	                          ,S.Name ActiveStatus
                              ,WDT.[CreatedAt]
                          FROM [DMS].[WorkflowDocumentType] WDT
                          INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
                         WHERE WDT.ActiveStatusId <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT WDT.[Id]
                              ,WDT.[WorkflowId]
                              ,WDT.[DocumentTypeId]
                              ,WDT.[ActiveStatusId]
	                          ,S.Name ActiveStatus
                              ,WDT.[CreatedAt]
                          FROM [DMS].[WorkflowDocumentType] WDT
                          INNER JOIN [Basic].[ActiveStatus] S on WDT.ActiveStatusId = S.ID
                         WHERE WDT.ActiveStatusId <> 3
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    request.Skip,
                    request.PageSize
                }))
                {
                    response = (await multi.ReadAsync<GetWorkFlowDocumentTypeQueryResult>()).ToList();
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
            else
            {
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
 WHERE WDT.ActiveStatusId <> 3 AND (@SearchValue is null OR ( WDT.[WorkflowId] like @SearchValue OR WDT.[DocumentTypeId] like @SearchValue OR S.Name like @SearchValue))
 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
OFFSET @Skip rows FETCH NEXT @Take rows only;
";
                using (var result = await connection.QueryMultipleAsync(query + countQuery, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    Take = request.PageSize,
                    request.Skip
                }))
                {
                    response = (await result.ReadAsync<GetWorkFlowDocumentTypeQueryResult>()).ToList();
                    totalCount = await result.ReadSingleAsync<int>();
                }
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
