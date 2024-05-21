using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowCompany;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowCompany;

public class WorkFlowCompanyQueryRepository : IWorkFlowCompanyQueryRepository
{
    private readonly string _connectionString;
    public WorkFlowCompanyQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetWorkFlowCompanyQueryResult> FindById(long id)
    {
        var response = new GetWorkFlowCompanyQueryResult();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                   SELECT DISTINCT C.[ID] as Id
                      ,C.[WorkFlowId]
                     ,C.[CompanyId]
                     ,C.[ActiveFrom]
                     ,C.[ActiveTo]
                     ,C.[activeStatusId]
                 FROM [PROJECT].[WORKFLOWCOMPANY] C
                 WHERE C.[ActiveStatusID] = 1 and C.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetWorkFlowCompanyQueryResult>(query, new { Id = id });
            response = result;
        }
        return response;
    }

    public async Task<Result<List<GetWorkFlowCompanyQueryResult>>> GetAll(GetAllWorkFlowCompanyQuery request)
    {
        var response = new List<GetWorkFlowCompanyQueryResult>();
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
                        SELECT DISTINCT C.[ID] as Id
                            ,C.[WorkFlowId]
                            ,C.[CompanyId]
                            ,C.[ActiveFrom]
                            ,C.[ActiveTo]
                            ,C.[activeStatusId]
                            ,c.[CreatedAt]
                        FROM [PROJECT].[WORKFLOWCOMPANY] C
                        WHERE C.[ActiveStatusID] <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT C.[ID] as Id
                            ,C.[WorkFlowId]
                            ,C.[CompanyId]
                            ,C.[ActiveFrom]
                            ,C.[ActiveTo]
                            ,C.[activeStatusId]
                            ,c.[CreatedAt]
                        FROM [PROJECT].[WORKFLOWCOMPANY] C
                        WHERE C.[ActiveStatusID] <> 3
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
                    response = (await multi.ReadAsync<GetWorkFlowCompanyQueryResult>()).ToList();
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
            else
            {
                string countQuery = @"
SELECT Count(*) Result
 FROM [PROJECT].[WORKFLOWCOMPANY] C
                 WHERE C.[ActiveStatusID] = 1
";
                string query = $@"
                   SELECT DISTINCT C.[ID] as Id
                     ,C.[WorkFlowId]
                     ,C.[CompanyId]
                     ,C.[ActiveFrom]
                     ,C.[ActiveTo]
                     ,C.[activeStatusId]
,c.[CreatedAt]
                 FROM [PROJECT].[WORKFLOWCOMPANY] C
                 WHERE C.[ActiveStatusID] <> 3
 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"} 
OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                using (var result = await connection.QueryMultipleAsync(query + countQuery, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.PageSize,
                    request.Skip
                }))
                {
                    response = (await result.ReadAsync<GetWorkFlowCompanyQueryResult>()).ToList();
                    totalCount = await result.ReadSingleAsync<int>();
                }
            }
        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }
}
