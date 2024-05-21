using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Common.Helper;
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
            if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
            {
                var splitedFilter = request.Filter.Split(":");
                string? SearchValue = splitedFilter[1].Trim().Sanitize();
                string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
                        SELECT DISTINCT C.[ID] as Id
                                      ,C.[Name]
                                      ,C.[Code]
                                      ,C.[WorkFlowId]
                                      ,C.[IsDirectManagerOfIssueCreator]
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
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT DISTINCT C.[ID] as Id
                                      ,C.[Name]
                                      ,C.[Code]
                                      ,C.[WorkFlowId]
                                      ,C.[IsDirectManagerOfIssueCreator]
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
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    request.Skip,
                    request.PageSize,
                    request.WorkFlowId,
                    request.ProjectId,
                    request.DomainId,
                }))
                {
                    var response = await multi.ReadAsync<GetWorkFlowActorQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                var queryCount = @"
                           SELECT COUNT(*) Result
                                  FROM [PROJECT].[WORKFLOWACTOR] C
                                  join [Project].[WorkFlow] W on C.WorkFlowId = W.Id
                            	  INNER JOIN [Project].[Project] P on w.ProjectID = P.Id
                            	  INNER JOIN [Authentication].[Domain] D on D.Id = P.DomainID
                            	  INNER JOIN [Basic].[ActiveStatus] A on A.ID = C.ActiveStatusID
                           WHERE  C.ActiveStatusId != 3
                            		and (@SearchValue is null OR C.[Name] like @SearchValue)
                            		AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR P.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId);";


                string query = $@"
                            SELECT DISTINCT C.[ID] as Id
                                      ,C.[Name]
                                      ,C.[Code]
                                      ,C.[WorkFlowId]
                                      ,C.[IsDirectManagerOfIssueCreator]
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
                            		and (@SearchValue is null OR C.[Name] like @SearchValue)
                            		 order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                            		OFFSET @Skip rows FETCH NEXT @Take rows only";


                using (var multi = await connection.QueryMultipleAsync(query + " " + queryCount, new
                {

                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    WorkFlowId = request.WorkFlowId,
                    ProjectId = request.ProjectId,
                    DomainId = request.DomainId,
                    Take = request.PageSize,
                    request.Skip,
                }))
                {
                    var response = await multi.ReadAsync<GetWorkFlowActorQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }
}
