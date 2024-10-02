using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowActor;

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
          ,C.[IsDirectManagerOfIssueCreator]
,c.IsActorManager
          ,C.[IsEveryOne]
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
                        FROM  [PROJECT].[WORKFLOWACTOR] C
                        left join  [Project].[WorkFlowActorGroup] AG on AG.WorkFlowActorID  = C.Id
                        join  [Authentication].[Groups] G on AG.GroupID = G.Id
                        WHERE C.[ActiveStatusID] = 1 and G.ActiveStatusId = 1 and C.Id = @Id
Order By c.[CreatedAt] desc  
                        
                        
                        
                        --ActorRole
                        SELECT DISTINCT R.[ID] as RoleId
                            ,R.[Name] as RoleName
                            ,R.[Code] as RoleCode
,c.[CreatedAt]
                        FROM  [PROJECT].[WORKFLOWACTOR] C
                        left join  [Project].[WorkFlowActorRole] AR on AR.WorkFlowActorID  = C.Id
                        join  [Authentication].Role R on AR.RoleID = R.Id
                        WHERE C.[ActiveStatusID] = 1 and R.ActiveStatusId = 1 and C.Id = @Id
                        Order By c.[CreatedAt] desc  
                        
                        
                        --ActorUser
                        SELECT DISTINCT U.[ID] as UserId
                            ,P.[FirstName] as FirstName
                            ,P.[LastName] as LastName
                        	,U.[Username] as UserName
,c.[CreatedAt]
                        FROM  [PROJECT].[WORKFLOWACTOR] C
                        left join  [Project].[WorkFlowActorUser] AU on AU.WorkFlowActorID  = C.Id
                        join  [Authentication].[Users] U on AU.UserID = U.Id
                        join  [Authentication].[Profile] P on U.ProfileID = P.Id
                        
                        WHERE C.[ActiveStatusID] = 1 and U.ActiveStatusId = 1 and C.Id = @Id
Order By c.[CreatedAt] desc  ";



            using (var multi = await connection.QueryMultipleAsync(query, new { Id = id }))
            {
                response = multi.ReadAsync<GetWorkFlowActorQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                response.WorkFlowActorGroups = multi.ReadAsync<WorkFlowActorGroup>().GetAwaiter().GetResult().ToList();
                response.WorkFlowActorRoles = multi.ReadAsync<WorkFlowActorRole>().GetAwaiter().GetResult().ToList();
                response.WorkFlowActorUsers = multi.ReadAsync<WorkFlowActorUser>().GetAwaiter().GetResult().ToList();
            }
        }
        return response;
    }
    public async Task<Result<IEnumerable<GetWorkFlowActorQueryResult>>> GetAll(GetAllWorkFlowActorsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
                var queryCount = @"  WITH Query as(
						         SELECT DISTINCT C.[ID] as Id
          ,C.[Name]
          ,C.[Code]
          ,C.[WorkFlowId]
          ,C.[IsDirectManagerOfIssueCreator]
          ,C.[IsEveryOne]
,c.IsActorManager
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
   WHERE  C.ActiveStatusId != 3
      		AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


                string query = $@" WITH Query as(
							 SELECT DISTINCT C.[ID] as Id
          ,C.[Name]
          ,C.[Code]
          ,C.[WorkFlowId]
          ,C.[IsDirectManagerOfIssueCreator]
,c.IsActorManager
          ,C.[IsEveryOne]
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
   WHERE  C.ActiveStatusId != 3
      		AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("WorkFlowId", request.WorkFlowId);
            dynaimcParameters.Item2.Add("ProjectId", request.ProjectId);
            dynaimcParameters.Item2.Add("DomainId", request.DomainId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetWorkFlowActorQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }
    public async Task<IEnumerable<GetWorkflowActorEmployeeQueryResult>> GetEmployee(long actorId)
    {
        var query = @"select wa.Name, wae.EmployeeId, wae.ActorId from project.WorkFlowActorEmployees wae
                        inner join project.WorkFlowActor wa on wa.Id = wae.EmployeeId
                        where wae.ActorId = @Id and wae.ActiveStatusId != 3 and wa.ActiveStatusId != 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<GetWorkflowActorEmployeeQueryResult>(query, new { Id = actorId });
        }
    }
    public async Task<bool> CheckAccessToIsEveryOne(long workFlowActorId)
    {
        var result = false;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var query = @" 
						select WA.* from Project.WorkFlowActor WA 
                        join Project.WorkFlowActorStep WAS on WA.Id = WAS.WorkFlowActorID 
                        join Project.Step S on S.Id = WAS.StepID
                        Join Project.Progress PS on PS.SourceId = WAS.StepID
                        join  Project.WorkFlowActorStep WAST on WAST.StepID = PS.TargetId
                        where WA.Id = @workFlowActorId and S.ActionTypeId = 9  and WA.ActiveStatusId != 3
						; "
            ;
            using (var multi = await connection.QueryMultipleAsync(query , new { workFlowActorId = workFlowActorId }))
            {

                var response = await multi.ReadAsync<GetWorkFlowActorQueryResult>();
                if(response  is not null) result = true;
            }

            return result;
        }
    }

}


