using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Persistance.Read.Repositories.Features.DMS.Documents;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues;

public class IssueQueryRepository : IIssueQueryRepository
{
    private readonly string _connectionString;
    private readonly IConfiguration _configuration;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IDocumentQueryRepository _repository;
    private readonly IFileService _fileService;

    public IssueQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity, IDocumentQueryRepository repository, IFileService fileService)
    {
        _connectionString = configuration.GetConnectionString();
        _configuration = configuration;
        _simaIdentity = simaIdentity;
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<Result<IEnumerable<GetAllIssueQueryResult>>> GetAll(GetAllIssuesQuery request)
    {
        var userId = _simaIdentity.UserId;
        var roleIds = _simaIdentity.RoleIds;
        var groupIds = _simaIdentity.GroupId;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @"   WITH Query as(
						   SELECT distinct 
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
       and I.ActiveStatusId != 3   and I.SourceId = 0
       AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId))
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
            string query = $@"
                             WITH Query as(
							 SELECT  distinct
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
       and I.ActiveStatusId != 3 and I.SourceId = 0
       AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId))
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("roleIds", roleIds);
            dynaimcParameters.Item2.Add("userId", userId);
            dynaimcParameters.Item2.Add("groupIds", groupIds);
            dynaimcParameters.Item2.Add("WorkFlowId", request.WorkFlowId);
            dynaimcParameters.Item2.Add("DomainId", request.DomainId);
            dynaimcParameters.Item2.Add("ProjectId", request.ProjectId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetAllIssueQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
    public async Task<Result<IEnumerable<GetAllIssueQueryResult>>> GetAllMyIssue(GetMyIssueListQuery request)
    {
        var userId = _simaIdentity.UserId;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @"   WITH Query as(
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
      WHERE  i.CreatedBy =@userId  and i.ActiveStatusId != 3 and i.SourceId = 0
      AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
            string query = $@" WITH Query as(
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
      WHERE  i.CreatedBy =@userId  and i.ActiveStatusId != 3 and i.SourceId = 0
      AND (@WorkFlowId is null OR W.Id = @WorkFlowId) AND (@DomainId is null OR project.DomainID = @DomainId) AND (@ProjectId is null OR w.ProjectID = @ProjectId)
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("userId", userId);
            dynaimcParameters.Item2.Add("WorkFlowId", request.WorkFlowId);
            dynaimcParameters.Item2.Add("DomainId", request.DomainId);
            dynaimcParameters.Item2.Add("ProjectId", request.ProjectId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetAllIssueQueryResult>();
                return Result.Ok(response, request, count);
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
    ,(Case
          When I.MainAggregateId = 2 Then
                (Select Code from SecurityCommitee.Meeting where Id = I.SourceId)
          Else null ENd) SourceCode
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
    ,S.HasDocument
    ,S.DisplayName as StepDisplayName
FROM  [IssueManagement].[Issue] I
 JOIN [Basic].[ActiveStatus] A on A.Id = I.ActiveStatusId
 JOIN  [Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
 JOIN  [Project].[Project] P On P.Id = W.ProjectID
 JOIN  [Authentication].[Domain] D on P.DomainID = D.Id
 JOIN  [IssueManagement].[IssueType] IT on IT.Id = I.IssueTypeId
 JOIN  [IssueManagement].[IssuePriority] IP on IP.Id = I.IssuePriorityId
LEFT JOIN  [IssueManagement].[IssueWeightCategory] IWC on IWC.Id = I.IssueWeightCategoryId
 JOIN  [Project].[Step] S on I.CurrenStepId = S.Id
LEFT JOIN  [Project].[State] ST on I.CurrentStateId = ST.Id
                      Where I.ActiveStatusId != 3 and I.Id = @Id 
                    
                      --IssueLink
                    
                        select 
                         IL.[IssueIdLinkedTo]
                        ,(ILT.[Summery] + ' - ' + ILT.Code) as IssueSummeryLinkedTo
                        ,IL.[IssueIdLinkReasonTo] as IssueLinkReasonId
                        ,ILR.[Name] as issueLinkReasonName
                        ,ILR.[Code] as issueLinkReasonCode
                         FROM  [IssueManagement].[Issue] I
                         Join  [IssueManagement].[IssueLink] IL on I.Id = IL.IssueId
                         join  [IssueManagement].[IssueLinkReason] ILR on ILR.Id = IL.IssueIdLinkReasonTo
                         Join  [IssueManagement].[Issue] ILT on IL.IssueIdLinkedTo = ILT.Id
                            Where I.ActiveStatusId != 3 and I.Id = @Id 
                    
                            -- StepRequiredDocument
                             SELECT 
                            	 dt.Id DocumentTypeId,
                            	 dt.Name DocumentTypeName,
                            	 srd.Count
                              FROM  [IssueManagement].[Issue] I
                              JOIN  [Project].[Step] S on I.CurrenStepId = S.Id
                              left join Project.StepRequiredDocument srd on srd.StepId = s.Id
                              left join DMS.DocumentType dt on dt.Id =srd.DocumentTypeId
                            Where I.ActiveStatusId != 3 and I.Id =  @Id and srd.ActiveStatusID <>3

                    	   --Document
                    	   select 
                    	   ID.[DocumentId]
                    	  ,D.[FileAddress] as  documentPath
                          ,D.[Name] as  Name
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

                        -------- Progress
                Select distinct
                    P.Id CurrentProgressId
                    ,FStep.Id ProgressId
                    ,s.id 
                    ,s.Name
                    ,P.HasStoreProcedure HasStoredProcedure
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
                    Where    I.Id = @Id  and  (WA.IsEveryOne = 1 or WU.UserID=@userId or WR.RoleId in @RoleIds or WG.GroupId in @GroupIds)
                    and I.ActiveStatusId <> 3 and P.ActiveStatusId <> 3 and S.ActiveStatusId <> 3 and ST.ActiveStatusId <> 3

                ----ProgressStoredProcedureParam
                
                select
                PSP.ProgressId
                ,PSPP.Id ProgressStoredProcedureParamId
                ,PSPP.Name 
                ,PSPP.DisplayName
                ,PSPP.JsonFormat
                ,PSPP.BoundFormat
                ,UI.IsMultiSelect
                ,UI.IsSingleSelect
                ,UI.HasInputInEachRecord
                ,UI.Name UiInputElementName
                ,UI.Id UiInputElementId
                ,PSPP.ApiNameForDataBounding
                ,PSPP.StoredProcedureForDataBounding
                From  
                        [Project].ProgressStoreProcedure PSP 
                        left join Project.ProgressStoreProcedureParam PSPP on PSP.Id = pspp.ProgressStoreProcedureId
                        left join Basic.UIInputElement  UI on UI.Id = pspp.UiInputElementId 
                		where Psp.ProgressId In
                		(
                		Select FStep.Id	From  [IssueManagement].[Issue] I
                        inner join  [Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
                        inner   join  [Project].[Step] S on I.CurrenStepId = S.Id
                        CROSS APPLY [Project].[ReturnNextStepN]   (s.id,w.Id) FStep
                        Where  I.Id = @Id
                		)


                        ----- ServiceInputName


                             Select distinct
	                                 IP.InputName ServiceInputName
	                                 ,IP.Id ServiceInputId
	                                 ,DT.Name DataTypeName
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
	                             left join [Project].[StepServiceTask] SST on ST.Id = SSt.StepId
	                             left join [Project].[ServiceTask] SerT on Sert.Id = SST.ServiceTaskId
	                             left join Basic.ApiMethodAction AM on AM.Id = SerT.ApiMethodActionId
	                             left join [Project].ServiceInputParam SIP on SIP.ServiceTaskId = SerT.Id
	                             left join [Project].InputParam IP on SIP.InputParamId = IP.Id
	                             left join Basic.DataType DT on DT.Id = IP.DataTypeId
                               Where    I.Id = @Id  and  (WA.IsEveryOne = 1 or WU.UserID=@userId or WR.RoleId in @RoleIds or WG.GroupId in @GroupIds)
                               and I.ActiveStatusId <> 3 and P.ActiveStatusId <> 3 and S.ActiveStatusId <> 3 and ST.ActiveStatusId <> 3

                               --ApprovalOption

                                EXEC [Project].[ReturnApprovalList] @Id";

            using (var multi = await connection.QueryMultipleAsync(query, new { Id = id, userId = userId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId }))
            {
                response = multi.ReadAsync<GetIssueQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
                response.IssueLinks = multi.ReadAsync<GetIssueLinkQueryResult>().GetAwaiter().GetResult().ToList();
                response.RequiredDocuments = multi.ReadAsync<GetStepRequiredDocumentQueryResult>().GetAwaiter().GetResult().ToList();
                response.IssueDocuments = multi.ReadAsync<GetIssueDocumentQueryResult>().GetAwaiter().GetResult().ToList();
                response.IssueComments = multi.ReadAsync<GetIssueCommentQueryResult>().GetAwaiter().GetResult().ToList();
                response.RelatedProgressList = multi.ReadAsync<GetRelatedProgressQueryResult>().GetAwaiter().GetResult().ToList();
                var storeProcedureParams = multi.ReadAsync<StoreProcedureParams>().GetAwaiter().GetResult()?.ToList();
                response.GetServiceInputList = multi.ReadAsync<GetServiceInputParam>().GetAwaiter().GetResult()?.ToList();
                response.ApprovalOptions = multi.ReadAsync<GetApprovalOptionQueryResult>().GetAwaiter().GetResult().ToList();

                if (response.RelatedProgressList is not null)
                {
                    foreach (var item in response.RelatedProgressList)
                    {
                        item.Params = storeProcedureParams.Where(x => x.ProgressId == item.ProgressId).ToList();

                    }
                }
            }
            response.NullCheck();
            return response;
        }
    }
    public async Task<GetIssueComponentQueryResult> ComponentIssue(long sourceId, long issueId)
    {
        try
        {
            var userId = _simaIdentity.UserId;
            var response = new GetIssueComponentQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                           ------ IssueInformation
                                     select
                                           I.Id
                                           ,I.Code
                                           ,I.CurrentWorkflowId WorkflowId
                                           ,W.Name WorkflowName
                                           ,W.FileContent WorkFlowFileContent
                                           ,I.MainAggregateId
                                           ,I.IssuePriorityId
                                           ,IP.Name IssuePriorityName
                                           ,I.IssueWeightCategoryId issueWeightId
                                           ,IWC.Name issueWeightName
                                           ,IWC.MaxRange Weight
                                           ,I.CurrentStateId
                                           ,State.Name CurrentStateName
                                           ,I.CurrenStepId CurrentStepId
                                           ,step.Name CurrentStepName
                                           ,i.DueDate
                                           ,Step.HasDocument
                                           ,Step.DisplayName as StepDisplayName
                                           ,Step.BpmnId CurrentStepBpmnId
                                           ,Step.UIPropertyBoxTitle
                                           ,case when I.CreatedBy = @userId then 1 
                                                when step.ActionTypeId=10 then 0 
                                            else 0 end as  IsEditable
                                           ,I.RequesterId
                                           ,(RP.FirstName + ' ' + rp.LastName) RequesterName
                                         FROM 
                                         IssueManagement.Issue I 
                                         INNER JOIN Project.WorkFlow W ON I.CurrentWorkflowId = W.Id
                                         INNER JOIN IssueManagement.IssuePriority IP ON IP.Id = I.IssuePriorityId
                                         LEFT JOIN IssueManagement.IssueWeightCategory IWC ON IWC.Id = I.IssueWeightCategoryId
                                         Left Join Project.State State on State.Id = i.CurrentStateId
                                         left Join Project.Step Step on Step.Id = i.CurrenStepId
                                         left join Project.WorkFlowActorStep WS on Step.Id = WS.StepID
                                        left join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                                        left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
                                        left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
                                        left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID
                                         left join Authentication.Users RU on RU.Id = I.RequesterId and RU.ActiveStatusId<>3
                                         left join Authentication.Profile RP on RP.Id = RU.ProfileID and RP.ActiveStatusId<>3
                                         WHERE  I.Id = @issueId AND I.ActiveStatusId <> 3
                                    
                                     ------ Comment
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
                                    Where I.ActiveStatusId != 3 and I.Id = @issueId
                                    order by IC.CreatedAt desc
                                    
                                  ------ IssueApproval
                                       select 
                                           SAO.Id StepApprovalOptionId
                                           ,IAP.Description
                                           ,Step2.Name StepName
                                           ,AO.Name StepApprovalOptionName
                                           ,Step2.Id StepId
                                           ,WA.Id ActorId
                                           ,WA.Name ActorName
                                           ,(p2.FirstName + ' ' + P2.LastName) CreatedBy
                                           ,(p3.FirstName + ' ' + P3.LastName) ApprovedBy
                                           ,IAP.CreatedAt
	                                       ,(select isnull(SignatureId,0) from Organization.Staff S
	   
	                                       where p3.id=S.ProfileId
	    
	                                       ) as SignatureId
                                      from 
                                       IssueManagement.Issue I 
                                      INNER JOIN IssueManagement.IssueApproval IAP on IAP.IssueId = I.Id
                                      INNER join Project.StepApprovalOption SAO on SAO.Id = IAP.StepApprovalOptionId
                                      INNER join Project.ApprovalOption AO on AO.Id = SAO.ApprovalOptionId
                                      INNER join Project.Step Step2 on Step2.Id = SAO.StepId
                                      INNER join Project.WorkFlowActorStep WAS on WAS.StepID = Step2.Id
                                      INNER join Project.WorkFlowActor WA on WA.Id = WAS.WorkFlowActorID
                                      INNER JOIN Authentication.Users U2 on IAP.CreatedBy = U2.Id
                                      INNER JOIN Authentication.Profile P2 on P2.Id = U2.ProfileID
                                      INNER JOIN Authentication.Users U3 on IAP.ApprovedBy = U3.Id
                                      INNER JOIN Authentication.Profile P3 on P3.Id = U3.ProfileID
                                      WHERE I.Id = @issueId AND I.ActiveStatusId <> 3
                                      order by CreatedAt
                                     ------ Progress
                                     Select distinct
                                          P.Id CurrentProgressId
                                          ,FStep.Id ProgressId
                                          ,s.id 
                                          ,s.Name
                                          ,P.HasStoreProcedure HasStoredProcedure
                                          ,FStep.Name TargetName
                                          ,case when FStep.TargetId = 0 then ST.Id else FStep.TargetId end as TargetId
                                          From  [IssueManagement].[Issue] I
                                          inner join  [Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
                                          inner   join  [Project].[Step] S on I.CurrenStepId = S.Id
                                          join Project.WorkFlowActorStep WS on S.Id = WS.StepID
                                          join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                                          left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID and WG.ActiveStatusId <> 3
                                          left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID  and WR.ActiveStatusId <> 3
                                          left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID and WU.ActiveStatusId <> 3
                                          inner  join  [Project].[Progress] P on S.Id = P.SourceId
                                          inner   join  [Project].[Step] ST on P.TargetId = ST.Id
                                          CROSS APPLY [Project].[ReturnNextStepN]   (s.id,w.Id) FStep
                                          left join  IssueManagement.IssueManager IM on IM.IssueId = I.id
                                          Where    I.Id = @issueId  and 
                                                    (( (ISNULL(IsDirectManagerOfIssueCreator,0)=0 and  (WU.UserID=@userId or WR.RoleID IN @RoleIds or WG.GroupID IN @GroupIds) )
                                                                      or (ISNULL(IsDirectManagerOfIssueCreator,0)=1 and IM.UserId=@userId)))
                                         and I.ActiveStatusId <> 3 and P.ActiveStatusId <> 3 and S.ActiveStatusId <> 3 and ST.ActiveStatusId <> 3
                                    
                                     ------ ProgressStoredProcedureParam
                                                 select
                                                    PSP.ProgressId
                                                    ,PSPP.Id ProgressStoredProcedureParamId
                                                    ,PSPP.Name 
                                                    ,PSPP.DisplayName
                                                    ,PSPP.ComboIsCascade
                                                    ,PSPP.JsonFormat
                                                    ,PSPP.BoundFormat
                                                    ,UI.IsMultiSelect
                                                    ,UI.IsSingleSelect
                                                    ,UI.HasInputInEachRecord
                                                    ,UI.Name UiInputElementName
                                                    ,UI.Id UiInputElementId
                                                    ,PSPP.ApiNameForDataBounding
                                                    ,PSPP.StoredProcedureForDataBounding
                                                    ,PSPP.[TextBoundName]
                                                    ,PSPP.[ValueBoundName]
  	                                                ,AMA.Name ApiMethodAction
  	                                                ,PSPP.[FixedValue]
                                                    From  
                                                            [Project].ProgressStoreProcedure PSP 
                                                            inner join Project.ProgressStoreProcedureParam PSPP on PSP.Id = pspp.ProgressStoreProcedureId and PSPP.ActiveStatusId <> 3
                                                            inner join Basic.UIInputElement  UI on UI.Id = pspp.UiInputElementId 
        			                                        left join Basic.ApiMethodAction AMA on AMA.Id = PSPP.ApiMethodActionId
                                    
                  		                                        where PSP.ActiveStatusId <> 3 and  Psp.ProgressId In 
                  		                                        (
                  		                                        SELECT FStep.Id 
                                                            FROM  
        			                                         [IssueManagement].[Issue] I
        			                                         INNER JOIN [Project].[WorkFlow] W ON I.CurrentWorkflowId = W.Id
        			                                         INNER JOIN [Project].[Step] S ON I.CurrenStepId = S.Id
        			                                         CROSS APPLY [Project].[ReturnNextStepN](S.Id, W.Id) FStep
           				                                         WHERE 
           				                                             I.Id = @issueId
           				                                         UNION 
           				                                         SELECT P.Id
           				                                         FROM  
           				                                             [IssueManagement].[Issue] I
           				                                             INNER JOIN [Project].[WorkFlow] W ON I.CurrentWorkflowId = W.Id
           				                                             INNER JOIN [Project].[Step] S ON I.CurrenStepId = S.Id
           				                                             INNER JOIN [Project].[Progress] P ON P.SourceId = S.Id
           				                                         WHERE 
           				                                             I.Id = @issueId
                  		                                        )
                                     ------ StepRequiredDocument
                                      SELECT 
                                            dt.Id DocumentTypeId,
                                            dt.Name DocumentTypeName,
                                            srd.Count
                                        FROM  [IssueManagement].[Issue] I
                                        JOIN  [Project].[Step] S on I.CurrenStepId = S.Id
                                        left join Project.StepRequiredDocument srd on srd.StepId = s.Id
                                        left join DMS.DocumentType dt on dt.Id =srd.DocumentTypeId
                                      Where I.ActiveStatusId != 3 and I.Id =  @issueId and srd.ActiveStatusID <>3
                                    
                                     ------ ApprovalOption
                                      EXEC [Project].[ReturnApprovalList] @issueId";
                using (var multi = await connection.QueryMultipleAsync(query, new { issueId = issueId, userId = _simaIdentity.UserId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId }))
                {
                    response.IssueInfo = multi.ReadAsync<IssueInfo>().GetAwaiter().GetResult()?.FirstOrDefault() ?? throw SimaResultException.NotFound;
                    if (!String.IsNullOrEmpty(response.IssueInfo.UIPropertyBoxTitle))
                        response.UIPropertyBoxTitle = response.IssueInfo.UIPropertyBoxTitle;

                    response.IssueInfo.IssueCommentList = (await multi.ReadAsync<GetIssueCommentQueryResult>())?.ToList();
                    response.IssueApprovalList = (await multi.ReadAsync<IssueApprovalList>())?.ToList();
                    response.RelatedProgressList = (await multi.ReadAsync<GetRelatedProgressQueryResult>())?.ToList();
                    var storeProcedureParams = (await multi.ReadAsync<StoreProcedureParams>())?.ToList();
                    var groupedStoreProcedureParams = storeProcedureParams?.GroupBy(x => x.Name);

                    response.StepRequiredDocumentList = (await multi.ReadAsync<GetStepRequiredDocumentQueryResult>())?.ToList();
                    response.ApprovalOptionList = (await multi.ReadAsync<GetApprovalOptionQueryResult>())?.ToList();

                    if (response.RelatedProgressList is not null)
                    {
                        response.FormParams = groupedStoreProcedureParams?.Select(x => new StoreProcedureParams
                        {
                            Name = x.Key,
                            ProgressStoredProcedureParamId = x.First().ProgressStoredProcedureParamId,
                            StoredProcedureForDataBounding = x.First().StoredProcedureForDataBounding,
                            ApiNameForDataBounding = x.First().ApiNameForDataBounding?.Replace("{Id}", sourceId.ToString()),
                            ApiMethodAction = x.First().ApiMethodAction,
                            DisplayName = x.First().DisplayName,
                            BoundFormat = x.First().BoundFormat,
                            HasInputInEachRecord = x.First().HasInputInEachRecord,
                            IsMultiSelect = x.First().IsMultiSelect,
                            IsSingleSelect = x.First().IsSingleSelect,
                            JsonFormat = x.First().JsonFormat,
                            ProgressId = x.First().ProgressId,
                            UiInputElementId = x.First().UiInputElementId,
                            ComboIsCascade = x.First().ComboIsCascade,
                            UiInputElementName = x.First().UiInputElementName,
                            TextBoundName = x.First().TextBoundName,
                            ValueBoundName = x.First().ValueBoundName,
                            FixedValue = x.First().FixedValue,
                            MultiDeserializedBoundFormat = x.First() is not null && !string.IsNullOrEmpty(x.First().BoundFormat) && x.First().UiInputElementId.HasValue && x.First().UiInputElementId.Value == 9 ? FillDataMulti(x.First().BoundFormat, sourceId) : null,
                            SingleDeserializedBoundFormat = x.First() is not null && !string.IsNullOrEmpty(x.First().BoundFormat) && x.First().UiInputElementId.HasValue && x.First().UiInputElementId.Value != 9 ? FillDataSingle(x.First().BoundFormat, sourceId) : null,
                            isCurrency = x.First().isCurrency,
                        });
                        foreach (var item in response.RelatedProgressList)
                        {
                            if (storeProcedureParams is not null)
                            {
                                var selectedParams = storeProcedureParams.Where(x => x.ProgressId == item.ProgressId).ToList();
                                var currentProject = storeProcedureParams.Where(x => x.ProgressId == item.CurrentProgressId).ToList();
                                if (selectedParams.Count != 0)
                                {
                                    item.Params = selectedParams;
                                }
                                if (currentProject.Count != 0 && selectedParams.Count == 0)
                                {
                                    item.Params = currentProject;
                                }
                                foreach (var param in item.Params)
                                {
                                    if (!string.IsNullOrEmpty(param.BoundFormat))
                                    {
                                        if (param.UiInputElementId.HasValue && param.UiInputElementId.Value == 9)
                                            param.MultiDeserializedBoundFormat = FillDataMulti(param.BoundFormat, sourceId);
                                        else
                                            param.SingleDeserializedBoundFormat = FillDataSingle(param.BoundFormat, sourceId);
                                    }
                                }
                            }
                        }
                    }



                    /// بنا به درخواست تیم تحلیل برای حوالجات نباید ویرایش داشته باشد
                    if (response.IssueInfo.MainAggregateId == (long)MainAggregateEnums.TrustyDraft)
                    {
                        response.IssueInfo.IsEditable = "0";
                    }


                    ///// در تاریخ 10 شهریور 1403، تیم تحلیل نظرش عوض شد و علی رغم داشتن متدی برای دریافت اطلاعات درخواست تدارکات و خرید برای کسی که ثبت کرده، دوباره این قسمت ریفکتور میشود
#region DisableUploadForCreator
//if (!string.IsNullOrEmpty(response.IssueInfo.IsEditable))
//{
//    if (string.Equals("1", response.IssueInfo.IsEditable, StringComparison.InvariantCultureIgnoreCase))
//    {
//        response.IssueInfo.HasDocument = "0";
//        response.FormParams = Enumerable.Empty<StoreProcedureParams>();
//        response.RelatedProgressList = Enumerable.Empty<GetRelatedProgressQueryResult>();
//    }
//}
#endregion
///// 
///
#if DEBUG
#else
                    foreach (var item in response.IssueApprovalList)
                    {
                        var documentResult = await _repository.GetForDownload(item.SignatureId, forIssueDetail: true);
                        if (documentResult != null)
                        {
                            var docResponse = new GetDownloadDocumentQueryResult();
                            var fileContent = await _fileService.Download(documentResult.FileAddress);
                            if (fileContent == null)
                            {
                                throw new SimaResultException(CodeMessges._404Code, Messages.DownloadError);
                            }
                            docResponse.FileContent = fileContent;
                            docResponse.Extension = documentResult.Extension;
                            docResponse.Name = documentResult.Name;
                            item.downloadedResult = docResponse;
                        }
                    }
#endif
                    /// توضیحات
                    /// if related progresses is empty, approvals should be empty as well. Analysys team says it should sent empty in this time 2025/01/19
                    /// توضیحات
                    #region Exceptions
                    if (response.RelatedProgressList is null || response.RelatedProgressList.Count() == 0)
                        response.ApprovalOptionList = null;
                    #endregion
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
    public async Task AddDocToSp(List<AddDocumentToSPQuery> docs)
    {
        var json = JsonConvert.SerializeObject(docs);
        var sp = $"[Logistics].[InsertLogisticstDocument] N'{json}'";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            try
            {
                await connection.ExecuteAsync(sp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public async Task UpdateDocuments(List<long> documentIds, long issueId, long currrentWorkflowId)
    {
        string script = @"";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

        }
    }

    public async Task<List<long>> GetIssueManager(long userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = @"
                        select UU.Id from Authentication.Users U
                        Join Organization.Staff S on U.ProfileId = S.ProfileId and S.ActiveStatusId != 3
                        join Organization.Staff SS on S.ManagerId = SS.Id and SS.ActiveStatusId != 3
                        join Authentication.Users UU on SS.ProfileId = UU.ProfileID  and UU.ActiveStatusId != 3
                        where U.Id = @userId and  U.ActiveStatusId != 3";
            var result = await connection.QueryAsync<long>(query, new { userId });
            return result.ToList();
        }
    }

    private IEnumerable<NewBoundFormatDeseralieze>? FillDataMulti(string? BoundFormat, long sourceId)
    {
        var returnedDataMulti = Enumerable.Empty<NewBoundFormatDeseralieze>();

        if (!string.IsNullOrEmpty(BoundFormat))
        {
            returnedDataMulti = JsonConvert.DeserializeObject<IEnumerable<NewBoundFormatDeseralieze>>(BoundFormat);
            foreach (var item in returnedDataMulti)
            {
                if (!string.IsNullOrEmpty(item.apiNameForDataBounding) && item.apiNameForDataBounding.Contains("/{Id}", StringComparison.InvariantCultureIgnoreCase))
                {
                    item.apiNameForDataBounding = item.apiNameForDataBounding.Replace("{Id}", sourceId.ToString(), StringComparison.InvariantCultureIgnoreCase);
                }
                if (!string.IsNullOrEmpty(item.boundFormat))
                {
                    item.singleDeserializedBoundFormat = JsonConvert.DeserializeObject<IEnumerable<BoundFormatDeserialized>>(item.boundFormat);
                    foreach (var item2 in item.singleDeserializedBoundFormat)
                    {
                        if (!string.IsNullOrEmpty(item2.apiNameForDataBounding) && item2.apiNameForDataBounding.Contains("/{Id}", StringComparison.InvariantCultureIgnoreCase))
                        {
                            item2.apiNameForDataBounding = item2.apiNameForDataBounding.Replace("{Id}", sourceId.ToString(), StringComparison.InvariantCultureIgnoreCase);
                        }
                    }
                }
            }
        }
        return returnedDataMulti;
    }

    private IEnumerable<BoundFormatDeserialized>? FillDataSingle(string? boundFormat, long sourceId)
    {
        var returnedDataSingle = Enumerable.Empty<BoundFormatDeserialized>();
        if (!string.IsNullOrEmpty(boundFormat))
        {

            returnedDataSingle = JsonConvert.DeserializeObject<IEnumerable<BoundFormatDeserialized>>(boundFormat);
            foreach (var item in returnedDataSingle)
            {
                if (!string.IsNullOrEmpty(item.apiNameForDataBounding) && item.apiNameForDataBounding.Contains("/{Id}", StringComparison.InvariantCultureIgnoreCase))
                {
                    item.apiNameForDataBounding = item.apiNameForDataBounding.Replace("{Id}", sourceId.ToString(), StringComparison.InvariantCultureIgnoreCase);
                }
            }
        }
        return returnedDataSingle;
    }
}