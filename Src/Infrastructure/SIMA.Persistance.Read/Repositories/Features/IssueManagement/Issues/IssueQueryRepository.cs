using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
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

        public async Task<Result<List<GetAllIssueQueryResult>>> GetAll(BaseRequest baseRequest, long? workFlowId = null)
        {
            var userId = _simaIdentity.UserId;
            var response = new List<GetAllIssueQueryResult>();
            int totalCount = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = string.Empty;
                if (!string.IsNullOrEmpty(baseRequest.SearchValue) && workFlowId is not null)
                {
                    query = @"
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
                                    from IssueManagement.Issue I  
                                    join Project.Step S on I.CurrenStepId = S.Id
                                    join Project.WorkFlowActorStep WS on S.Id = WS.StepID
                                    join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                                    left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                                    left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                                    left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                                    left join Project.State ST on I.CurrentStateId = ST.Id
                                    join Project.WorkFlow W on I.CurrentWorkflowId = W.Id
                                    left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                                    left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                                    join [SIMADB].[Authentication].[Users] U on I.CreatedBy = U.Id
                                    join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID

                        WHERE  WR.RoleID in @RoleIds or WG.GroupID in @GroupIds or  WU.UserID=@userId
                                and I.ActiveStatusId != 3
                            and (I.[Summery] like @SearchValue or I.[Code] like @SearchValue or I.[Description] like @SearchValue)
AND W.Id = @WorkFlowId
 order by I.CreatedAt desc
                    ";
                    var result = await connection.QueryAsync<GetAllIssueQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%", userId = userId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId, WorkFlowId = workFlowId });
                    totalCount = result.Count();
                    response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                   .Take(baseRequest.Take)
                    .ToList();
                }
                else if (!string.IsNullOrEmpty(baseRequest.SearchValue) && workFlowId is null)
                {
                    query = @"
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
                                    from IssueManagement.Issue I  
                                    join Project.Step S on I.CurrenStepId = S.Id
                                    join Project.WorkFlowActorStep WS on S.Id = WS.StepID
                                    join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                                    left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                                    left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                                    left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                                    left join Project.State ST on I.CurrentStateId = ST.Id
                                    join Project.WorkFlow W on I.CurrentWorkflowId = W.Id
                                    left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                                    left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                                    join [SIMADB].[Authentication].[Users] U on I.CreatedBy = U.Id
                                    join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID

                        WHERE  WR.RoleID in @RoleIds or WG.GroupID in @GroupIds or  WU.UserID=@userId
                                and I.ActiveStatusId != 3
                            and (I.[Summery] like @SearchValue or I.[Code] like @SearchValue or I.[Description] like @SearchValue)
 order by I.CreatedAt desc
                    ";
                    var result = await connection.QueryAsync<GetAllIssueQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%", userId = userId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId });
                    totalCount = result.Count();
                    response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                   .Take(baseRequest.Take)
                    .ToList();
                }
                else
                {
                    if (workFlowId is null)
                    {
                        query = @"
                                SELECT  
                                      I.Id,I.Code,I.[CurrentWorkflowId]  WorkflowId
                                    ,W.Name workflowname,I.mainAggregateId,I.issueTypeId,IT.Name issueTypeName
                                    ,I.issuePriorityId,IPP.Name issuePriorityName
                                    ,I.weight,I.currentStateId
                                    ,ST.Name currentStateName
                                    ,S.id currentStepId
                                    ,S.Name currentStepName
                                    ,I.summery,I.description
                                    ,P.[FirstName] as PerformerFirstName
                                    ,P.[LastName] as PerformerLastName
                                    ,I.[CreatedAt] 
                                    from IssueManagement.Issue I  
                                    join Project.Step S on I.CurrenStepId = S.Id
                                    join Project.WorkFlowActorStep WS on S.Id = WS.StepID
                                    join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                                    left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                                    left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                                    left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                                    left join Project.State ST on I.CurrentStateId = ST.Id
                                    join Project.WorkFlow W on I.CurrentWorkflowId = W.Id
                                    left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                                    left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                                    join [SIMADB].[Authentication].[Users] U on I.CreatedBy = U.Id
                                    join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID
                                            WHERE  WR.RoleID in @RoleIds or WG.GroupID in @GroupIds or  WU.UserID=@userId
                                                    and I.ActiveStatusId != 3
                                          order by I.CreatedAt desc";
                        var result = await connection.QueryAsync<GetAllIssueQueryResult>(query, new { userId = userId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId });
                        totalCount = result.Count();
                        response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                       .Take(baseRequest.Take)
                       .ToList();
                    }
                    else
                    {
                        query = @"
                                SELECT  
                                      I.Id,I.Code,I.[CurrentWorkflowId] WorkflowId
                                    ,W.Name workflowname,I.mainAggregateId,I.issueTypeId,IT.Name issueTypeName
                                    ,I.issuePriorityId,IPP.Name issuePriorityName
                                    ,I.weight,I.currentStateId
                                    ,ST.Name currentStateName
                                    ,S.id currentStepId
                                    ,S.Name currentStepName
                                    ,I.summery,I.description
                                    ,P.[FirstName] as PerformerFirstName
                                    ,P.[LastName] as PerformerLastName
                                    ,I.[CreatedAt] 
                                    from IssueManagement.Issue I  
                                    join Project.Step S on I.CurrenStepId = S.Id
                                    join Project.WorkFlowActorStep WS on S.Id = WS.StepID
                                    join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                                    left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                                    left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                                    left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                                    left join Project.State ST on I.CurrentStateId = ST.Id
                                    join Project.WorkFlow W on I.CurrentWorkflowId = W.Id
                                    left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                                    left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                                    join [SIMADB].[Authentication].[Users] U on I.CreatedBy = U.Id
                                    join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID
                                            WHERE  WR.RoleID in @RoleIds or WG.GroupID in @GroupIds or  WU.UserID=@userId
                                                    and I.ActiveStatusId != 3
AND W.Id = @WorkFlowId
                                          order by I.CreatedAt desc";
                        var result = await connection.QueryAsync<GetAllIssueQueryResult>(query, new { userId = userId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId, WorkFlowId = workFlowId });
                        totalCount = result.Count();
                        response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                       .Take(baseRequest.Take)
                       .ToList();
                    }

                    
                }

            }
            return Result.Ok(response, totalCount); ;
        }

        public async Task<Result<List<GetAllIssueQueryResult>>> GetAllMyIssue(BaseRequest baseRequest, long? workFlowId = null)
        {
            try
            {
                var userId = _simaIdentity.UserId;
                var response = new List<GetAllIssueQueryResult>();
                int totalCount = 0;
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = string.Empty;
                    if (!string.IsNullOrEmpty(baseRequest.SearchValue) && workFlowId is null)
                    {
                        query = @"
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
                        -- ,i.CreatedAt
                        -- ,i.CreatedBy
                        , S.Name as ActiveStatus
                        ,P.[FirstName] as FirstName
                        ,P.[LastName] as LastName
                        ,I.[CreatedAt] 
                        FROM IssueManagement.Issue i
                        INNER JOIN Basic.ActiveStatus S on S.ID = i.ActiveStatusId
                        inner join Project.WorkFlow w on w.Id= i.CurrentWorkflowId
                        inner join IssueManagement.IssueType ist on ist.Id= i.IssueTypeId
                        right outer join IssueManagement.IssuePriority ipp on  ipp.Id=i.IssuePriorityId 
                        LEFT OUTER JOIN Project.State ST  on ST.Id=i.CurrentStateId
                        LEFT OUTER JOIN Project.Step SP  on SP.Id=i.CurrenStepId
                        join [SIMADB].[Authentication].[Users] U on I.CreatedBy = U.Id
                        join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID
                WHERE  i.CreatedAt =@userId  and i.ActiveStatusId != 3
                    and (i.[Summery] like @SearchValue or i.[Code] like @SearchValue or i.[Description] like @SearchValue)
                   order by i.CreatedAt desc";
                        var result = await connection.QueryAsync<GetAllIssueQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%", userId });
                        totalCount = result.Count();
                        response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                       .Take(baseRequest.Take)
                        .ToList();
                    }
                    else if (!string.IsNullOrEmpty(baseRequest.SearchValue) && workFlowId is not null)
                    {
                        query = @"
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
                        -- ,i.CreatedAt
                        -- ,i.CreatedBy
                        , S.Name as ActiveStatus
                        ,P.[FirstName] as FirstName
                        ,P.[LastName] as LastName
                        ,I.[CreatedAt] 
                        FROM IssueManagement.Issue i
                        INNER JOIN Basic.ActiveStatus S on S.ID = i.ActiveStatusId
                        inner join Project.WorkFlow w on w.Id= i.CurrentWorkflowId
                        inner join IssueManagement.IssueType ist on ist.Id= i.IssueTypeId
                        right outer join IssueManagement.IssuePriority ipp on  ipp.Id=i.IssuePriorityId 
                        LEFT OUTER JOIN Project.State ST  on ST.Id=i.CurrentStateId
                        LEFT OUTER JOIN Project.Step SP  on SP.Id=i.CurrenStepId
                        join [SIMADB].[Authentication].[Users] U on I.CreatedBy = U.Id
                        join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID
                WHERE  i.CreatedAt =@userId  and i.ActiveStatusId != 3
                    and (i.[Summery] like @SearchValue or i.[Code] like @SearchValue or i.[Description] like @SearchValue)
and w.Id = @WorkFlowId
                   order by i.CreatedAt desc";
                        var result = await connection.QueryAsync<GetAllIssueQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%", userId, WorkFlowId = workFlowId });
                        totalCount = result.Count();
                        response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                       .Take(baseRequest.Take)
                        .ToList();
                    }
                    else
                    {
                        if (workFlowId is null)
                        {
                            query = @"
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
                        -- ,i.CreatedAt
                        -- ,i.CreatedBy
                        , S.Name as ActiveStatus
                        ,P.[FirstName] as FirstName
                        ,P.[LastName] as LastName
                        ,I.[CreatedAt] 
                        FROM IssueManagement.Issue i
                        INNER JOIN Basic.ActiveStatus S on S.ID = i.ActiveStatusId
                        inner join Project.WorkFlow w on w.Id= i.CurrentWorkflowId
                        inner join IssueManagement.IssueType ist on ist.Id= i.IssueTypeId
                        right outer join IssueManagement.IssuePriority ipp on  ipp.Id=i.IssuePriorityId 
                        LEFT OUTER JOIN Project.State ST  on ST.Id=i.CurrentStateId
                        LEFT OUTER JOIN Project.Step SP  on SP.Id=i.CurrenStepId
                        join [SIMADB].[Authentication].[Users] U on I.CreatedBy = U.Id
                        join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID
                WHERE  i.CreatedBy =@userId  and i.ActiveStatusId != 3
                order by i.CreatedAt desc";

                            var result = await connection.QueryAsync<GetAllIssueQueryResult>(query, new { userId });
                            totalCount = result.Count();
                            response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                           .Take(baseRequest.Take)
                           .ToList();
                        }
                        else
                        {
                            query = @"
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
                        -- ,i.CreatedAt
                        -- ,i.CreatedBy
                        , S.Name as ActiveStatus
                        ,P.[FirstName] as FirstName
                        ,P.[LastName] as LastName
                        ,I.[CreatedAt] 
                        FROM IssueManagement.Issue i
                        INNER JOIN Basic.ActiveStatus S on S.ID = i.ActiveStatusId
                        inner join Project.WorkFlow w on w.Id= i.CurrentWorkflowId
                        inner join IssueManagement.IssueType ist on ist.Id= i.IssueTypeId
                        right outer join IssueManagement.IssuePriority ipp on  ipp.Id=i.IssuePriorityId 
                        LEFT OUTER JOIN Project.State ST  on ST.Id=i.CurrentStateId
                        LEFT OUTER JOIN Project.Step SP  on SP.Id=i.CurrenStepId
                        join [SIMADB].[Authentication].[Users] U on I.CreatedBy = U.Id
                        join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID
                WHERE  i.CreatedBy =@userId  and i.ActiveStatusId != 3 AND w.Id = @WorkFlowId
                order by i.CreatedAt desc";

                            var result = await connection.QueryAsync<GetAllIssueQueryResult>(query, new { userId, WorkFlowId = workFlowId });
                            totalCount = result.Count();
                            response = result.Skip((baseRequest.Skip - 1) * baseRequest.Take)
                           .Take(baseRequest.Take)
                           .ToList();
                        }
                    }

                }
                return Result.Ok(response, totalCount); ;
            }
            catch (Exception ex)
            {
                throw;
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
                        FROM [SIMADB].[IssueManagement].[Issue] I
                        join [SIMADB].[Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
                        join [SIMADB].[Project].[Project] P On P.Id = W.ProjectID
                        join [SIMADB].[Authentication].[Domain] D on P.DomainID = D.Id
                        Join [SIMADB].[IssueManagement].[IssueType] IT on IT.Id = I.IssueTypeId
                        Join [SIMADB].[IssueManagement].[IssuePriority] IP on IP.Id = I.IssuePriorityId
                        Join [SIMADB].[IssueManagement].[IssueWeightCategory] IWC on IWC.Id = I.IssueWeightCategoryId
                        join [SIMADB].[Project].[Step] S on I.CurrenStepId = S.Id
                        left join [SIMADB].[Project].[State] ST on I.CurrentStateId = ST.Id
                      Where I.ActiveStatusId != 3 and I.Id = @Id 
                    
                      --IssueLink
                    
                        select 
                         IL.[IssueIdLinkedTo]
                        ,ILT.[Summery] as IssueSummeryLinkedTo
                        ,IL.[IssueIdLinkReasonTo] as IssueLinkReasonId
                        ,ILR.[Name] as issueLinkReasonName
                        ,ILR.[Code] as issueLinkReasonCode
                         FROM [SIMADB].[IssueManagement].[Issue] I
                         Join [SIMADB].[IssueManagement].[IssueLink] IL on I.Id = IL.IssueId
                         join [SIMADB].[IssueManagement].[IssueLinkReason] ILR on ILR.Id = IL.IssueIdLinkReasonTo
                         Join [SIMADB].[IssueManagement].[Issue] ILT on IL.IssueIdLinkedTo = ILT.Id
                            Where I.ActiveStatusId != 3 and I.Id = @Id 
                    
                    	   --Document
                    	   select 
                    	   ID.[DocumentId]
                    	  ,D.[FileAddress] as  documentPath
                    	  ,DE.[Name] as documentExtentionName
                    	  FROM [SIMADB].[IssueManagement].[Issue] I
                    	    left join [SIMADB].[IssueManagement].[IssueDocument] ID on I.Id = ID.IssueId
                            left join [SIMADB].[DMS].[Documents] D on D.Id= ID.DocumentId
                            join [SIMADB].[DMS].[DocumentExtension] DE on DE.Id = D.FileExtensionId
                           Where I.ActiveStatusId != 3 and I.Id = @Id 
                    		 --Comment
                    		select 
                    	  IC.[Id]
                    	  ,IC.[Comment]
                    	  ,IC.[CreatedAt]
                    	  ,IC.[CreatedBy]
                    	  ,U.[Username]
                    	  FROM [SIMADB].[IssueManagement].[Issue] I
                    	    join [SIMADB].[IssueManagement].[IssueComment] IC on I.Id = IC.IssueId
                            join [SIMADB].[Authentication].[Users] U on Ic.CreatedBy = U.Id
                         Where I.ActiveStatusId != 3 and I.Id = @Id
                         order by IC.CreatedAt desc

                         --Progress
                            Select
                               s.id 
                               ,s.Name 
                               ,FStep.Name TargetName
                               ,case when FStep.TargetId = 0 then ST.Id else FStep.TargetId end as TargetId
                               From [SIMADB].[IssueManagement].[Issue] I
                               inner join [SIMADB].[Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
                               inner   join [SIMADB].[Project].[Step] S on I.CurrenStepId = S.Id
                               join Project.WorkFlowActorStep WS on S.Id = WS.StepID
	                           join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
	                           left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
	                           left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
	                           left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                               inner  join [SIMADB].[Project].[Progress] P on S.Id = P.SourceId
                                 inner   join [SIMADB].[Project].[Step] ST on P.TargetId = ST.Id
                                       CROSS APPLY [Project].[ReturnNextStepN]   (s.id,w.Id) FStep

                              Where    I.Id = @Id and  (WU.UserID=@userId or WR.RoleId in @RoleIds or WG.GroupId in @GroupIds) and
                               I.ActiveStatusId <> 3";
                try
                {
                    using (var multi = await connection.QueryMultipleAsync(query, new { Id = id, userId = userId ,RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId }))
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
                catch
                {
                    throw;
                }
               
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
                           FROM [SIMADB].[IssueManagement].[IssueHistory] I
						  join [SIMADB].[Project].[Step] S on S.Id = I.SourceStepId
						  left join [SIMADB].[Project].[State] ST on ST.Id = I.SourceStateId
						  join [SIMADB].[Project].[Step] STA on STA.Id = I.TargetStepId
						  left join [SIMADB].[Project].[State] STTA on STTA.Id = I.TargetStateId
						  join [SIMADB].[Authentication].[Users] U on I.PerformerUserId = U.Id
						  join [SIMADB].[Authentication].[Profile] P on P.Id = U.ProfileID WHERE IssueId = @IssueId
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
                          FROM [SIMADB].[IssueManagement].[IssueHistory]
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
    }
}
