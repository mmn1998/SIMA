using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.Origins;
using SIMA.Application.Query.Contract.Features.BCP.Scenarios;
using SIMA.Application.Query.Contract.Features.BCP.SenarioExecutionHistories;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read.Repositories.Features.BCP.Scenarios;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.SenarioExecutionHistories
{
    public class SenarioExecutionHistoryQueryRepository : ISenarioExecutionHistoryQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public SenarioExecutionHistoryQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"
                     Select 
                          SEH.Id 
	                     ,SEH.ScenarioId
                         ,S.Code ScenarioCode
	                     ,S.Title ScenarioTitle
	                     ,SEH.ExecutionNumber
	                     ,SEH.ExecutionDate
	                     ,SEH.ExecutionTimeFrom
	                     ,SEH.ExecutionTimeTo
                         ,SEH.Description
                         ,S.CreatedAt
                         ,P.FirstName + ' ' + P.LastName as CreateBy
                     From BCP.ScenarioExecutionHistory SEH
                     join BCP.Scenario S on SEH.ScenarioId = S.Id and S.ActiveStatusId <> 3
                     join Authentication.Users U on SEH.CreatedBy = U.Id
                     join Authentication.Profile P on U.ProfileID = P.Id
                     where SEH.ActiveStatusId <> 3";
        }

        public async Task<Result<IEnumerable<GetSenarioExecutionHistoryQueryResult>>> GetAll(GetAllSenarioExecutionHistoriesQuery request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


                string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
                var dynaimcParameters = (queryCount + query).GenerateQuery(request);
                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetSenarioExecutionHistoryQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }

        public async Task<GetSenarioExecutionHistoryQueryResult> GetById(GetSenarioExecutionHistoryQuery request)
        {
            var query = $@"
          Select 
                          SEH.Id 
	                     ,SEH.ScenarioId
                         ,S.Code ScenarioCode
	                     ,S.Title ScenarioTitle
	                     ,SEH.ExecutionNumber
	                     ,SEH.ExecutionDate
	                     ,SEH.ExecutionTimeFrom
	                     ,SEH.ExecutionTimeTo
                         ,SEH.Description
                         ,S.CreatedAt
                         ,P.FirstName + ' ' + P.LastName as CreateBy
                     From BCP.ScenarioExecutionHistory SEH
                     join BCP.Scenario S on SEH.ScenarioId = S.Id and S.ActiveStatusId <> 3
                     join Authentication.Users U on SEH.CreatedBy = U.Id
                     join Authentication.Profile P on U.ProfileID = P.Id
                     where SEH.ActiveStatusId <> 3 and SEH.Id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstAsync<GetSenarioExecutionHistoryQueryResult>(query, new { request.Id });
                result.NullCheck();
                return result ?? throw SimaResultException.NotFound;
            }
        }
    }
}
