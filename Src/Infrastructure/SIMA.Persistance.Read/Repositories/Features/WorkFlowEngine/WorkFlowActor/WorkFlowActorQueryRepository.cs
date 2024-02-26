using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Persistance.Read;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowActor
{
    public class WorkFlowActorQueryRepository : IWorkFlowActorQueryRepository
    {
        private readonly string _connectionString;


        public WorkFlowActorQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }
        public async Task<GetWorkFlowActorQueryResult> FindById(long id)
        {
            var response = new GetWorkFlowActorQueryResult();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
                  SELECT DISTINCT C.[ID] as Id
          ,C.[Name]
          ,C.[Code]
         	,C.[WorkFlowId]
         	,W.[Name] as WorkFlowName
          ,C.[activeStatusId]
		  ,A.[Name] ActiveStatus
		  ,P.[Name] ProjectName
		  ,P.Id ProjectId
		  ,D.[Name] DomainName
		  ,D.[Id] DomainId
,c.[CreatedAt]
      FROM [PROJECT].[WORKFLOWACTOR] C
      join [Project].[WorkFlow] W on C.WorkFlowId = W.Id
	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
      WHERE C.[ActiveStatusID] <> 3 AND C.Id = @Id
         Order By c.[CreatedAt] desc               
                        
                        --ActorGroup
                        SELECT DISTINCT G.[ID] as GroupId
                            ,G.[Name] as GroupName
                            ,G.[Code] as GroupCode
,c.[CreatedAt]
                        FROM [SIMADB].[PROJECT].[WORKFLOWACTOR] C
                        left join [SIMADB].[Project].[WorkFlowActorGroup] AG on AG.WorkFlowActorID  = C.Id
                        join [SIMADB].[Authentication].[Groups] G on AG.GroupID = G.Id
                        WHERE C.[ActiveStatusID] = 1 and G.ActiveStatusId = 1 and C.Id = @Id
Order By c.[CreatedAt] desc  
                        
                        
                        
                        --ActorRole
                        SELECT DISTINCT R.[ID] as RoleId
                            ,R.[Name] as RoleName
                            ,R.[Code] as RoleCode
,c.[CreatedAt]
                        FROM [SIMADB].[PROJECT].[WORKFLOWACTOR] C
                        left join [SIMADB].[Project].[WorkFlowActorRole] AR on AR.WorkFlowActorID  = C.Id
                        join [SIMADB].[Authentication].Role R on AR.RoleID = R.Id
                        WHERE C.[ActiveStatusID] = 1 and R.ActiveStatusId = 1 and C.Id = @Id
                        Order By c.[CreatedAt] desc  
                        
                        
                        --ActorUser
                        SELECT DISTINCT U.[ID] as UserId
                            ,P.[FirstName] as FirstName
                            ,P.[LastName] as LastName
                        	,U.[Username] as UserName
,c.[CreatedAt]
                        FROM [SIMADB].[PROJECT].[WORKFLOWACTOR] C
                        left join [SIMADB].[Project].[WorkFlowActorUser] AU on AU.WorkFlowActorID  = C.Id
                        join [SIMADB].[Authentication].[Users] U on AU.UserID = U.Id
                        join [SIMADB].[Authentication].[Profile] P on U.ProfileID = P.Id
                        
                        WHERE C.[ActiveStatusID] = 1 and U.ActiveStatusId = 1 and C.Id = @Id
Order By c.[CreatedAt] desc  ";
                        


                using (var multi = await connection.QueryMultipleAsync(query, new { Id = id }))
                {
                    response = multi.ReadAsync<GetWorkFlowActorQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ;
                    response.WorkFlowActorGroups = multi.ReadAsync<WorkFlowActorGroup>().GetAwaiter().GetResult().ToList();
                    response.WorkFlowActorRoles = multi.ReadAsync<WorkFlowActorRole>().GetAwaiter().GetResult().ToList();
                    response.WorkFlowActorUsers = multi.ReadAsync<WorkFlowActorUser>().GetAwaiter().GetResult().ToList();
                }
            }
            return response;
        }

        public async Task<List<GetWorkFlowActorQueryResult>> GetAll()
        {
            try
            {

                var response = new List<GetWorkFlowActorQueryResult>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = $@"
SELECT DISTINCT C.[ID] as Id
          ,C.[Name]
          ,C.[Code]
         	,C.[WorkFlowId]
         	,W.[Name] as WorkFlowName
          ,C.[activeStatusId]
		  ,A.[Name] ActiveStatus
		  ,P.[Name] ProjectName
		  ,P.Id ProjectId
		  ,D.[Name] DomainName
		  ,D.[Id] DomainId
,c.[CreatedAt]
      FROM [PROJECT].[WORKFLOWACTOR] C
      join [Project].[WorkFlow] W on C.WorkFlowId = W.Id
	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
      WHERE C.[ActiveStatusID] <> 3   
Order By c.[CreatedAt] desc  
";
                    var result = await connection.QueryAsync<GetWorkFlowActorQueryResult>(query);
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
