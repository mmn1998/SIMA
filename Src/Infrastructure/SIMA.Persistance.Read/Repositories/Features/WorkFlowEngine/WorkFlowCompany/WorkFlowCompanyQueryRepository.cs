using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
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

    public async Task<Result<IEnumerable<GetWorkFlowCompanyQueryResult>>> GetAll(GetAllWorkFlowCompanyQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
                string queryCount = @"WITH Query as(
						                            SELECT DISTINCT C.[ID] as Id
                     ,C.[WorkFlowId]
                     ,C.[CompanyId]
                     ,C.[ActiveFrom]
                     ,C.[ActiveTo]
                     ,C.[activeStatusId]
,c.[CreatedAt]
                 FROM [PROJECT].[WORKFLOWCOMPANY] C
                 WHERE C.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;
";
                string query = $@"
                   WITH Query as(
							                   SELECT DISTINCT C.[ID] as Id
                     ,C.[WorkFlowId]
                     ,C.[CompanyId]
                     ,C.[ActiveFrom]
                     ,C.[ActiveTo]
                     ,C.[activeStatusId]
,c.[CreatedAt]
                 FROM [PROJECT].[WORKFLOWCOMPANY] C
                 WHERE C.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetWorkFlowCompanyQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }
}
