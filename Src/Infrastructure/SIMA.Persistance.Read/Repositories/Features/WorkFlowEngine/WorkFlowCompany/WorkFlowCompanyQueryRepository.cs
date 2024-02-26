using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowCompany;
using SIMA.Persistance.Read;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowCompany
{
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

        public async Task<List<GetWorkFlowCompanyQueryResult>> GetAll()
        {
            try
            {

                var response = new List<GetWorkFlowCompanyQueryResult>();

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
,c.[CreatedAt]
                 FROM [PROJECT].[WORKFLOWCOMPANY] C
                 WHERE C.[ActiveStatusID] = 1
Order By c.[CreatedAt] desc  ";
                    var result = await connection.QueryAsync<GetWorkFlowCompanyQueryResult>(query);
                    response = result.ToList();
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }


        }


    }
}
