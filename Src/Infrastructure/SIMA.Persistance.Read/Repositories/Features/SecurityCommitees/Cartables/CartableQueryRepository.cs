using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.Cartables;

public class CartableQueryRepository : ICartableQueryRepository
{
    private readonly string _connectionString;
    private readonly ISimaIdentity _simaIdentity;

    public CartableQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity)
    {
        _connectionString = configuration.GetConnectionString();
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<List<GetAllCartableQueryResult>>> GetAll(GetAllCartableQuery request)
    {
        var response = new List<GetAllCartableQueryResult>();
        int totalCount = 0;
        var userId = _simaIdentity.UserId;
        var roleIds = _simaIdentity.RoleIds;
        var groupIds = _simaIdentity.GroupId;

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
                        SELECT  
                            M.Id, M.Code,M.Description,
                            I.Id IssueId,I.Code IssueCode,I.[CurrentWorkflowId] WorkflowId
                           ,W.Name workflowname,I.mainAggregateId,I.issueTypeId,IT.Name issueTypeName
                           ,I.issuePriorityId,IPP.Name issuePriorityName
                           ,I.weight,I.currentStateId
                           ,ST.Name currentStateName
                           ,S.id currentStepId
                           ,S.Name currentStepName -- Button for Detail
                           ,I.summery,I.description IssueDescription
                           ,P.[FirstName] as FirstName
                           ,P.[LastName] as LastName
                           ,M.[CreatedAt] 
                           ,M.[CreatedBy] 
                           from 
	                       SecurityCommitee.Meeting M 
	                       JOIN IssueManagement.Issue I  on I.Id= M.IssueId and i.SourceId=M.Id AND I.MainAggregateId = 2
                           JOIN Project.Step S on I.CurrenStepId = S.Id
                           JOIN Project.WorkFlowActorStep WS on S.Id = WS.StepID
                           JOIN Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                           LEFT JOIN Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                           LEFT JOIN Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                           LEFT JOIN  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                           LEFT JOIN Project.State ST on I.CurrentStateId = ST.Id
                           JOIN Project.WorkFlow W on I.CurrentWorkflowId = W.Id
	                       JOIN Project.Project project on project.Id = W.ProjectID
                           LEFT JOIN [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                           LEFT JOIN [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                           JOIN  [Authentication].[Users] U on I.CreatedBy = U.Id
                           JOIN  [Authentication].[Profile] P on P.Id = U.ProfileID
                          
	                    WHERE  ((WR.RoleID IN @roleIds or WG.GroupID IN @groupIds or  WU.UserID=@userId)
                        and I.ActiveStatusId != 3 and M.ActiveStatusId != 3 and W.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT  
                            M.Id, M.Code,M.Description,
                            I.Id IssueId,I.Code IssueCode,I.[CurrentWorkflowId] WorkflowId
                           ,W.Name workflowname,I.mainAggregateId,I.issueTypeId,IT.Name issueTypeName
                           ,I.issuePriorityId,IPP.Name issuePriorityName
                           ,I.weight,I.currentStateId
                           ,ST.Name currentStateName
                           ,S.id currentStepId
                           ,S.Name currentStepName -- Button for Detail
                           ,I.summery,I.description IssueDescription
                           ,P.[FirstName] as FirstName
                           ,P.[LastName] as LastName
                           ,M.[CreatedAt] 
                           ,M.[CreatedBy] 
                           FROM 
	                       SecurityCommitee.Meeting M 
	                       JOIN IssueManagement.Issue I  on I.Id= M.IssueId and i.SourceId=M.Id AND I.MainAggregateId = 2
                           JOIN Project.Step S on I.CurrenStepId = S.Id
                           JOIN Project.WorkFlowActorStep WS on S.Id = WS.StepID
                           JOIN Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                           LEFT JOIN Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                           LEFT JOIN Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                           LEFT JOIN  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
                           LEFT JOIN Project.State ST on I.CurrentStateId = ST.Id
                           JOIN Project.WorkFlow W on I.CurrentWorkflowId = W.Id
	                       JOIN Project.Project project on project.Id = W.ProjectID
                           LEFT JOIN [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                           LEFT JOIN [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                           JOIN  [Authentication].[Users] U on I.CreatedBy = U.Id
                           JOIN  [Authentication].[Profile] P on P.Id = U.ProfileID
                          
	                    WHERE  ((WR.RoleID IN @roleIds or WG.GroupID IN @groupIds or  WU.UserID=@userId)
                        and I.ActiveStatusId != 3 and M.ActiveStatusId != 3 and W.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    userId,
                    roleIds,
                    groupIds,
                    request.Skip,
                    request.PageSize
                }))
                {
                    response = (await multi.ReadAsync<GetAllCartableQueryResult>()).ToList();
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
            else
            {
                string queryCount = $@"
                                    SELECT COUNT(*) Result
                                      FROM SecurityCommitee.Meeting M 
	                                  join IssueManagement.Issue I  on I.Id= M.IssueId and i.SourceId=M.Id AND I.MainAggregateId = 2
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
                                      and I.ActiveStatusId != 3 and M.ActiveStatusId != 3 and W.ActiveStatusId != 3
                                      and (@SearchValue is null OR I.[Summery] like @SearchValue or I.[Code] like @SearchValue or I.[Description] like @SearchValue)
                                      AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId))";
                string query = $@"
                           SELECT  
                            M.Id, M.Code,M.Description,
                            I.Id IssueId,I.Code IssueCode,I.[CurrentWorkflowId] WorkflowId
                           ,W.Name workflowname,I.mainAggregateId,I.issueTypeId,IT.Name issueTypeName
                           ,I.issuePriorityId,IPP.Name issuePriorityName
                           ,I.weight,I.currentStateId
                           ,ST.Name currentStateName
                           ,S.id currentStepId
                           ,S.Name currentStepName -- Button for Detail
                           ,I.summery,I.description IssueDescription
                           ,P.[FirstName] as FirstName
                           ,P.[LastName] as LastName
                           ,M.[CreatedAt] 
                           ,M.[CreatedBy] 
                           from 
	                       SecurityCommitee.Meeting M 
	                       join IssueManagement.Issue I  on I.Id= M.IssueId and i.SourceId=M.Id AND I.MainAggregateId = 2
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
                           and I.ActiveStatusId != 3 and M.ActiveStatusId != 3 and W.ActiveStatusId != 3
                           and (@SearchValue is null OR I.[Summery] like @SearchValue or I.[Code] like @SearchValue or I.[Description] like @SearchValue)
                           --AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId))
                           order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}            
                           OFFSET @Skip rows FETCH NEXT @Take rows only ;";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    userId,
                    roleIds,
                    groupIds,
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    //request.WorkFlowId,
                    //request.DomainId,
                    //request.ProjectId,
                    request.Skip,
                    request.PageSize

                }))
                {
                    response = (await multi.ReadAsync<GetAllCartableQueryResult>()).ToList();
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
        }
        return Result.Ok(response, totalCount, request.PageSize, request.Page);
    }

    public async Task<GetCartableQueryResult> GetDetail(GetCartableQuery request)
    {
        var response = new GetCartableQueryResult();

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
	  ,F.JsonContent
	  ,f.Id FormId
	  ,f.Name FormName
	  ,f.Title FormTitle
	  ,f.IsSystemForm
  FROM  
  
  
  SecurityCommitee.Meeting M 
 join IssueManagement.Issue I  on I.Id= M.IssueId and i.SourceId=M.Id
  join [Basic].[ActiveStatus] A on A.Id = I.ActiveStatusId
  join  [Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
  join  [Project].[Project] P On P.Id = W.ProjectID
  join  [Authentication].[Domain] D on P.DomainID = D.Id
  Join  [IssueManagement].[IssueType] IT on IT.Id = I.IssueTypeId
  Join  [IssueManagement].[IssuePriority] IP on IP.Id = I.IssuePriorityId
  Join  [IssueManagement].[IssueWeightCategory] IWC on IWC.Id = I.IssueWeightCategoryId
  join  [Project].[Step] S on I.CurrenStepId = S.Id
   LEFT join Authentication.Form f on s.FormId = f.Id
  left join  [Project].[State] ST on I.CurrentStateId = ST.Id
Where M.ActiveStatusId != 3 and I.ActiveStatusId != 3 and W.ActiveStatusId != 3 and M.Id=@Id 
                    
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
      Where I.ActiveStatusId != 3 and I.Id = @IssueId 
                    
 	   --Document
 	   select 
 	   ID.[DocumentId]
 	  ,D.[FileAddress] as  documentPath
 	  ,DE.[Name] as documentExtentionName
 	  FROM  [IssueManagement].[Issue] I
 	    left join  [IssueManagement].[IssueDocument] ID on I.Id = ID.IssueId
      left join  [DMS].[Documents] D on D.Id= ID.DocumentId
      join  [DMS].[DocumentExtension] DE on DE.Id = D.FileExtensionId
     Where I.ActiveStatusId != 3 and I.Id = @IssueId 
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
   Where I.ActiveStatusId != 3 and I.Id = @IssueId
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
         Where    I.Id = @IssueId and  (WU.UserID=@userId or WR.RoleId in @RoleIds or WG.GroupId in @GroupIds) and
         I.ActiveStatusId <> 3 and P.ActiveStatusId <> 3 and S.ActiveStatusId <> 3 and ST.ActiveStatusId <> 3 and M.ActiveStatusId <> 3 "";
    "
        ;
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var multi = await connection.QueryMultipleAsync(query,
                new { Id = request.MeetingId, IssueId = request.IssueId, userId = _simaIdentity.UserId,
                    RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId }))
            {
                response = multi.ReadAsync<GetCartableQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.IssueErrorException);
                response.IssueLinks = multi.ReadAsync<GetIssueLinkQueryResult>().GetAwaiter().GetResult().ToList();
                response.IssueDocuments = multi.ReadAsync<GetIssueDocumentQueryResult>().GetAwaiter().GetResult().ToList();
                response.IssueComments = multi.ReadAsync<GetIssueCommentQueryResult>().GetAwaiter().GetResult().ToList();
                response.RelatedProgresses = multi.ReadAsync<GetRelatedProgressQueryResult>().GetAwaiter().GetResult().ToList();
            }
        }

        return response;
    }
}
