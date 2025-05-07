using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.Scenarios;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.Scenarios;

public class ScenarioQueryRepository : IScenarioQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ScenarioQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
                    Select 
                         S.Id 
                        ,S.Code
                        ,S.Title
                        ,S.CreatedAt
                        ,P.FirstName + ' ' + P.LastName as CreatedBy
                    From BCP.Scenario S
                    join Authentication.Users U on S.CreatedBy = U.Id
                    join Authentication.Profile P on U.ProfileID = P.Id
                    where S.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetScenarioQueryResult>>> GetAll(GetAllScenariosQuery request)
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
                var response = await multi.ReadAsync<GetScenarioQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetScenarioQueryResult> GetById(GetScenarioQuery request)
    {
        try
        {
            var response = new GetScenarioQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                           Select 
                             S.Id 
                            ,S.Code
                            ,S.Title
                            ,S.CreatedAt
                            ,P.FirstName + ' ' + P.LastName as CreatedBy
                            From BCP.Scenario S
                            join Authentication.Users U on S.CreatedBy = U.Id
                            join Authentication.Profile P on U.ProfileID = P.Id
                            where S.ActiveStatusId <> 3 and S.Id = @Id


                            ------scenarioBusinessContinuityPlanVersioning

                            Select 
                             SV.Id
                            ,BCP.Title BusinessContinuityPlanTitle
                            ,BV.VersionNumber
                            ,BV.ReleaseDate
                            ,SV.CreatedAt
                            From BCP.Scenario S
                            join BCP.[ ScenarioBusinessContinuityPlanVersioning] SV on S.Id = SV.ScenarioId and SV.ActiveStatusId <> 3
                            join BCP.BusinessContinuityPlanVersioning BV on SV.BusinessContinuityPlanVersioningId = BV.Id
                            join BCP.BusinessContinuityPlan BCP on BCP.Id = BV.BusinessContinuityPlanId
                            where S.ActiveStatusId <> 3 and S.Id = @Id



                            ------scenarioBusinessContinuityPlanAssumption
                            Select 
                             SA.Id
                            ,BA.Title 
                            ,BA.Code
                            From BCP.Scenario S
                            join BCP.[ScenarioBusinessContinuityPlanAssumption] SA on S.Id = SA.ScenarioId and SA.ActiveStatusId <> 3
                            join BCP.BusinessContinuityPlanAssumption BA on SA.BusinessContinuityPlanAssumptionId = BA.Id and BA.ActiveStatusId <> 3
                            where S.ActiveStatusId <> 3 and S.Id = @Id 

                            -------scenarioPlanRecoveryCriteria

                            Select 
                            SC.Id
                            ,SC.Description 
                            ,SC.CreatedAt
                            from BCP.Scenario S
                            join BCP.ScenarioRecoveryCriteria SC on S.Id = SC.ScenarioId and SC.ActiveStatusId <> 3
                            where S.ActiveStatusId <> 3 and S.Id = @Id 



                            -------scenarioPossibleAction

                            Select 
                            SA.Id
                            ,SA.Description 
                            ,SA.CreatedAt
                            from BCP.Scenario S
                            join BCP.ScenarioPossibleAction SA on S.Id = SA.ScenarioId and SA.ActiveStatusId <> 3
                            where S.ActiveStatusId <> 3 and S.Id = @Id 

                            -------scenarioRecoveryOption

                            Select 
                             SO.Id
                            ,SO.Description 
                            ,SO.RecoveryOptionPriorityId
                            ,ROP.Name RecoveryOptionPriorityName
                            ,SO.CreatedAt
                            from BCP.Scenario S
                            join BCP.ScenarioRecoveryOption SO on S.Id = SO.ScenarioId and SO.ActiveStatusId <> 3
                            join BCP.RecoveryOptionPriority ROP on SO.RecoveryOptionPriorityId = ROP.Id
                            where S.ActiveStatusId <> 3 and S.Id = @Id 


        ";

                using (var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id }))
                {
                    response = multi.ReadAsync<GetScenarioQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                    response.ScenarioBusinessContinuityPlanVersioningList = multi.ReadAsync<GetScenarioBusinessContinuityPlanVersioning>().GetAwaiter().GetResult().ToList();
                    response.ScenarioBusinessContinuityPlanAssumptionList = multi.ReadAsync<GetScenarioBusinessContinuityPlanAssumption>().GetAwaiter().GetResult().ToList();
                    response.ScenarioPlanRecoveryCriteriaList = multi.ReadAsync<GetScenarioPlanRecoveryCriteria>().GetAwaiter().GetResult().ToList();
                    response.ScenarioPossibleActionList = multi.ReadAsync<GetScenarioPossibleAction>().GetAwaiter().GetResult().ToList();
                    response.ScenarioRecoveryOptionList = multi.ReadAsync<GetScenarioRecoveryOption>().GetAwaiter().GetResult().ToList();
                }
                response.NullCheck();
                return response;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
