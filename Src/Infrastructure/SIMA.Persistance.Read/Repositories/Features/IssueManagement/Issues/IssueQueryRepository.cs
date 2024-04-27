using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues
{
    public class IssueQueryRepository : IIssueQueryRepository
    {

        private readonly string _connectionString;
        private readonly ISimaIdentity _simaIdentity;

        public IssueQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity)
        {
            _connectionString = configuration.GetConnectionString();
            _simaIdentity = simaIdentity;
        }

        public async Task<Result<IEnumerable<GetAllIssueQueryResult>>> GetAll(GetAllIssuesQuery request)
        {
            var userId = _simaIdentity.UserId;
            var roleIds = _simaIdentity.RoleIds;
            var groupIds = _simaIdentity.GroupId;
            
            using (var connection = new SqlConnection(_connectionString))
            {
                var queryCount = @" SELECT  COUNT(*) Result
                                    from IssueManagement.Issue I  
                                    join Project.Step S on I.CurrenStepId = S.Id
                                    join Project.WorkFlowActorStep WS on S.Id = WS.StepID
                                    join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                                    left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                                    left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                                    left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                                    left join Project.State ST on I.CurrentStateId = ST.Id
                                    join Project.WorkFlow W on I.CurrentWorkflowId = W.Id
									join Project.Project project on project.Id = W.ProjectID
                                    left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                                    left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                                    join  [Authentication].[Users] U on I.CreatedBy = U.Id
                                    join  [Authentication].[Profile] P on P.Id = U.ProfileID
                                    WHERE  ((WR.RoleID IN @roleIds or WG.GroupID IN @groupIds or  WU.UserID=@userId)
                                    and I.ActiveStatusId != 3
                                    and (@SearchValue is null OR I.[Summery] like @SearchValue or I.[Code] like @SearchValue or I.[Description] like @SearchValue)
                                    AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId));";
                await connection.OpenAsync();
                string query = $@"
                              SELECT  
                                     I.Id,I.Code,I.[CurrentWorkflowId] WorkflowId
                                    ,W.Name workflowname,I.mainAggregateId,I.issueTypeId,IT.Name issueTypeName
                                    ,I.issuePriorityId,IPP.Name issuePriorityName
                                    ,I.weight,I.currentStateId
                                    ,ST.Name currentStateName
                                    ,S.id currentStepId
                                    ,S.Name currentStepName
                                    ,I.summery,I.description
                                    ,P.[FirstName] as FirstName
                                    ,P.[LastName] as LastName
                                    ,I.[CreatedAt] 
                                    ,I.[CreatedBy] 
                                    from IssueManagement.Issue I  
                                    join Project.Step S on I.CurrenStepId = S.Id
                                    join Project.WorkFlowActorStep WS on S.Id = WS.StepID
                                    join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                                    left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                                    left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                                    left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                                    left join Project.State ST on I.CurrentStateId = ST.Id
                                    join Project.WorkFlow W on I.CurrentWorkflowId = W.Id
									join Project.Project project on project.Id = W.ProjectID
                                    left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                                    left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                                    join  [Authentication].[Users] U on I.CreatedBy = U.Id
                                    join  [Authentication].[Profile] P on P.Id = U.ProfileID
                                    WHERE  ((WR.RoleID IN @roleIds or WG.GroupID IN @groupIds or  WU.UserID=@userId)
                                    and I.ActiveStatusId != 3
                                    and (@SearchValue is null OR I.[Summery] like @SearchValue or I.[Code] like @SearchValue or I.[Description] like @SearchValue)
                                    AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId))
                                    order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    userId,
                    roleIds,
                    SearchValue = "%" + request.Filter + "%",
                    groupIds,
                    request.WorkFlowId,
                    request.DomainId,
                    request.ProjectId,
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetAllIssueQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }

        public async Task<Result<IEnumerable<GetAllIssueQueryResult>>> GetAllMyIssue(GetMyIssueListQuery request)
        {
            var userId = _simaIdentity.UserId;
            
            using (var connection = new SqlConnection(_connectionString))
            {
                var queryCount = @"
                                   SELECT COUNT(*) Result
                                         FROM IssueManagement.Issue i
                                         INNER JOIN Basic.ActiveStatus S on S.ID = i.ActiveStatusId
                                         inner join Project.WorkFlow w on w.Id= i.CurrentWorkflowId
                                 		join Project.Project project on project.Id = W.ProjectID
                                         inner join IssueManagement.IssueType ist on ist.Id= i.IssueTypeId
                                         right outer join IssueManagement.IssuePriority ipp on  ipp.Id=i.IssuePriorityId 
                                         LEFT OUTER JOIN Project.State ST  on ST.Id=i.CurrentStateId
                                         LEFT OUTER JOIN Project.Step SP  on SP.Id=i.CurrenStepId
                                         join  [Authentication].[Users] U on I.CreatedBy = U.Id
                                         join  [Authentication].[Profile] P on P.Id = U.ProfileID
                                         WHERE  i.CreatedBy =@userId  and i.ActiveStatusId != 3
                                             and (@SearchValue is null OR i.[Summery] like @SearchValue or i.[Code] like @SearchValue or i.[Description] like @SearchValue)
                                         AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)";
                await connection.OpenAsync();
                string query = $@"
                                SELECT DISTINCT 
                                      i.Id
                                      ,i.Code
                                      ,i.CurrentWorkflowId WorkflowId
                                      ,w.Name WorkflowName
                                      ,i.MainAggregateId
                                      ,i.SourceId
                                      ,i.IssueTypeId
                                      ,ist.Name IssueTypeName
                                      ,i.IssuePriorityId
                                      ,ipp.Name IssuePriorityName
                                      ,i.Weight
                                      ,i.CurrentStateId
                                      ,ST.Name currentStateName
                                      ,i.CurrenStepId
                                      ,SP.Name CurrentStepName
                                      ,i.Summery
                                      ,i.Description
                                      -- ,i.IssueDate
                                      -- ,i.DueDate
                                      ,i.ActiveStatusId
                                      , S.Name as ActiveStatus
                                      ,P.[FirstName] as FirstName
                                      ,P.[LastName] as LastName
                                      ,I.[CreatedAt] 
                                      FROM IssueManagement.Issue i
                                      INNER JOIN Basic.ActiveStatus S on S.ID = i.ActiveStatusId
                                      inner join Project.WorkFlow w on w.Id= i.CurrentWorkflowId
                              	      join Project.Project project on project.Id = W.ProjectID
                                      inner join IssueManagement.IssueType ist on ist.Id= i.IssueTypeId
                                      right outer join IssueManagement.IssuePriority ipp on  ipp.Id=i.IssuePriorityId 
                                      LEFT OUTER JOIN Project.State ST  on ST.Id=i.CurrentStateId
                                      LEFT OUTER JOIN Project.Step SP  on SP.Id=i.CurrenStepId
                                      join [Authentication].[Users] U on I.CreatedBy = U.Id
                                      join [Authentication].[Profile] P on P.Id = U.ProfileID
                                      WHERE  i.CreatedBy =@userId  and i.ActiveStatusId != 3
                                      and (@SearchValue is null OR i.[Summery] like @SearchValue or i.[Code] like @SearchValue or i.[Description] like @SearchValue)
                                      AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
                                      order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                      OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    userId,
                    SearchValue = "%" + request.Filter + "%",
                    request.WorkFlowId,
                    request.DomainId,
                    request.ProjectId,
                    request.Skip,
                    request.PageSize,
                }))
                {
                    var response = await multi.ReadAsync<GetAllIssueQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }

        public async Task<GetIssueQueryResult> GetById(long id)
        {
            var userId = _simaIdentity.UserId;
            var response = new GetIssueQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                           SELECT  I.[Id]
                            ,I.[CurrentWorkflowId]
                         	,W.[Name] As WorkFlowName
                        	,P.[Name] as ProjectName
                        	,D.[Name] as DomainName
                            ,I.[MainAggregateId]
                            ,I.[SourceId]
                            ,I.[IssueTypeId]
                            ,IT.[Name] As IssueTypeName
                            ,I.[IssuePriorityId]
                         	,IP.[Name] As IssuePriorityName
                            ,I.[IssueWeightCategoryId]
                            ,IWC.[Name] as IssueWeightCategoryName
                            ,I.[CurrenStepId] as CurrentStepId
                            ,S.[Name] As CurrentStepName
                            ,I.[CurrentStateId]
                         	,ST.[Name] As CurrentStateName
                            ,I.[Weight]
                            ,I.[Code]
                            ,I.[Summery]
                            ,I.[Description]
                        	,I.[DueDate]
                            ,S.BpmnId BpmnId
                            ,W.FileContent WorkFlowFileContent 
                            ,A.Name ActiveStatus    
                            ,I.ActiveStatusId
                        FROM  [IssueManagement].[Issue] I
                        join [Basic].[ActiveStatus] A on A.Id = I.ActiveStatusId
                        join  [Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
                        join  [Project].[Project] P On P.Id = W.ProjectID
                        join  [Authentication].[Domain] D on P.DomainID = D.Id
                        Join  [IssueManagement].[IssueType] IT on IT.Id = I.IssueTypeId
                        Join  [IssueManagement].[IssuePriority] IP on IP.Id = I.IssuePriorityId
                        Join  [IssueManagement].[IssueWeightCategory] IWC on IWC.Id = I.IssueWeightCategoryId
                        join  [Project].[Step] S on I.CurrenStepId = S.Id
                        left join  [Project].[State] ST on I.CurrentStateId = ST.Id
                      Where I.ActiveStatusId != 3 and I.Id = @Id 
                    
                      --IssueLink
                    
                        select 
                         IL.[IssueIdLinkedTo]
                        ,ILT.[Summery] as IssueSummeryLinkedTo
                        ,IL.[IssueIdLinkReasonTo] as IssueLinkReasonId
                        ,ILR.[Name] as issueLinkReasonName
                        ,ILR.[Code] as issueLinkReasonCode
                         FROM  [IssueManagement].[Issue] I
                         Join  [IssueManagement].[IssueLink] IL on I.Id = IL.IssueId
                         join  [IssueManagement].[IssueLinkReason] ILR on ILR.Id = IL.IssueIdLinkReasonTo
                         Join  [IssueManagement].[Issue] ILT on IL.IssueIdLinkedTo = ILT.Id
                            Where I.ActiveStatusId != 3 and I.Id = @Id 
                    
                    	   --Document
                    	   select 
                    	   ID.[DocumentId]
                    	  ,D.[FileAddress] as  documentPath
                    	  ,DE.[Name] as documentExtentionName
                    	  FROM  [IssueManagement].[Issue] I
                    	    left join  [IssueManagement].[IssueDocument] ID on I.Id = ID.IssueId
                            left join  [DMS].[Documents] D on D.Id= ID.DocumentId
                            join  [DMS].[DocumentExtension] DE on DE.Id = D.FileExtensionId
                           Where I.ActiveStatusId != 3 and I.Id = @Id 
                    		 --Comment
                    		select 
                    	  IC.[Id]
                    	  ,IC.[Comment]
                    	  ,IC.[CreatedAt] CommentDate
                    	  ,IC.[CreatedBy]
                    	  ,U.[Username]
                          ,(P.FirstName + ' ' + P.LastName) CreatorFullname
                    	  FROM [IssueManagement].[Issue] I
                    	    join [IssueManagement].[IssueComment] IC on I.Id = IC.IssueId
                            join [Authentication].[Users] U on Ic.CreatedBy = U.Id
                            join [Authentication].[Profile] P on P.Id = U.ProfileID
                         Where I.ActiveStatusId != 3 and I.Id = @Id
                         order by IC.CreatedAt desc

                         --Progress
                            Select

                               FStep.Id ProgressId
                               ,s.id 
                               ,s.Name 
                               ,FStep.Name TargetName
                               ,case when FStep.TargetId = 0 then ST.Id else FStep.TargetId end as TargetId
                               From  [IssueManagement].[Issue] I
                               inner join  [Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
                               inner   join  [Project].[Step] S on I.CurrenStepId = S.Id
                               join Project.WorkFlowActorStep WS on S.Id = WS.StepID
	                           join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
	                           left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
	                           left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
	                           left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                               inner  join  [Project].[Progress] P on S.Id = P.SourceId
                               inner   join  [Project].[Step] ST on P.TargetId = ST.Id
                               CROSS APPLY [Project].[ReturnNextStepN]   (s.id,w.Id) FStep
                               Where    I.Id = @Id and  (WU.UserID=@userId or WR.RoleId in @RoleIds or WG.GroupId in @GroupIds) and
                               I.ActiveStatusId <> 3 and P.ActiveStatusId <> 3 and S.ActiveStatusId <> 3 and ST.ActiveStatusId <> 3 ";
                using (var multi = await connection.QueryMultipleAsync(query, new { Id = id, userId = userId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId }))
                {
                    response = multi.ReadAsync<GetIssueQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ?? throw IssueExceptions.IssueErrorException;
                    response.IssueLinks = multi.ReadAsync<GetIssueLinkQueryResult>().GetAwaiter().GetResult().ToList();
                    response.IssueDocuments = multi.ReadAsync<GetIssueDocumentQueryResult>().GetAwaiter().GetResult().ToList();
                    response.IssueComments = multi.ReadAsync<GetIssueCommentQueryResult>().GetAwaiter().GetResult().ToList();
                    response.RelatedProgresses = multi.ReadAsync<GetRelatedProgressQueryResult>().GetAwaiter().GetResult().ToList();
                }
                response.NullCheck();
                return response;
            }
        }

        public async Task<IEnumerable<GetIssueHistoriesByIssueIdQueryResult>> GetIssueHistoryByIssueId(long issueId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"SELECT I.[Id]
                              ,I.[Name]
                              ,I.[IssueId]
                              ,I.[SourceStateId]
                              ,I.[TargetStateId]
                              ,I.[SourceStepId]
                              ,I.[TargetStepId]
                              ,I.[PerformerUserId]
                              ,I.[Description]
                              ,I.[CreatedAt]
                              ,I.[CreatedBy]
							  ,S.[Name] as CurrentStepName
							  ,ST.[Name] as CurrentStateName
							  ,STA.[Name] as TargetStepName 
							  ,STTA.[Name] as TargetStateName 
							  ,P.[FirstName] as PerformerFirstName
							  ,P.[LastName] as PerformerLastName
                           FROM  [IssueManagement].[IssueHistory] I
						  join  [Project].[Step] S on S.Id = I.SourceStepId
						  left join  [Project].[State] ST on ST.Id = I.SourceStateId
						  join  [Project].[Step] STA on STA.Id = I.TargetStepId
						  left join  [Project].[State] STTA on STTA.Id = I.TargetStateId
						  join  [Authentication].[Users] U on I.PerformerUserId = U.Id
						  join  [Authentication].[Profile] P on P.Id = U.ProfileID WHERE IssueId = @IssueId
                            order by I.CreatedAt desc";
                var result = await connection.QueryAsync<GetIssueHistoriesByIssueIdQueryResult>(query, new { IssueId = issueId });
                return result;
            }
        }
        public async Task<GetIssueHistoriesByIdQueryResult> GetIssueHistoryById(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"SELECT TOP (1) [Id]
                              ,[Name]
                              ,[IssueId]
                              ,[SourceStateId]
                              ,[TargetStateId]
                              ,[SourceStepId]
                              ,[TargetStepId]
                              ,[PerformerUserId]
                              ,[Description]
                              ,[CreatedAt]
                              ,[CreatedBy]
                          FROM  [IssueManagement].[IssueHistory]
                          WHERE Id = @Id";
                var result = await connection.QueryFirstOrDefaultAsync<GetIssueHistoriesByIdQueryResult>(query, new { Id = id });
                return result;
            }
        }
        public async Task<bool> IsCodeUnique(string code, long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "select top 1 1 FROM IssueManagement.Issue where id<>@Id and Code = @Code";
                var result = await connection.QuerySingleOrDefaultAsync<bool>(query, new { Id = id, Code = code });
                return result;
            }
        }

        public async Task<List<GetCasesByWorkflowIdQueryResult>> GetCasesByWorkflowId(long workflowId)
        {
            var result = new List<GetCasesByWorkflowIdQueryResult>();
            string getMainAggregateIdQuery = @"
        SELECT [MainAggregateId]
        FROM [Project].[WorkFlow]
        Where Id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                int mainAggregateId = connection.QueryFirst<int>(getMainAggregateIdQuery, new { Id = workflowId });
                string mainQuery = string.Empty;
                switch (mainAggregateId)
                {
                    case (int)MainAggregateEnums.SecurityCommitee:
                        {
                            mainQuery = @"
                            SELECT  [Id]
                                  ,([Description] + ' '  + [Code]) as Title
                              FROM [SecurityCommitee].[Meeting]
                            ";
                        }
                        break;
                    default:
                        break;
                }
                result = (await connection.QueryAsync<GetCasesByWorkflowIdQueryResult>(mainQuery)).ToList();
            }
            return result;
        }
    }
}
