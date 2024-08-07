using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues;

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
                        JOIN  [IssueManagement].[IssueWeightCategory] IWC on IWC.Id = I.IssueWeightCategoryId
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
                response.ApprovalOptions = multi.ReadAsync<GetApprovalOptionQueryResult>().GetAwaiter().GetResult().ToList();

                if (response.RelatedProgressList is not null)
                {
                    foreach (var item in response.RelatedProgressList)
                    {
                        item.Params = storeProcedureParams.Where(x => x.ProgressId == item.ProgressId).ToList();

                    }
                }
            }
            //var paramList = new List<StoreProcedureParams>();
            //var groupedResults = response.RelatedProgresses.GroupBy(x => new { x.ProgressId/*, x.ProgressStoreProcedureId */})
            //    .Select(g => new
            //    {
            //        ProgressId = g.Key.ProgressId,
            //        // ProgressStoreProcedureId = g.Key.ProgressStoreProcedureId
            //        Items = g.ToList()
            //    });
            //foreach (var progress in response.RelatedProgresses)
            //{
            //    if (progress.HasStoredProcedure == "1" && !string.IsNullOrEmpty(progress.ProcedureInfo) && progress.ProcedureInfo.Contains(":"))
            //    {
            //        var obj = new StoreProcedureParams();
            //        /// <summary>
            //        /// splitedinfo[0] is ProgressStoreProcedureId, splitedinfo[1] is Name, splitedinfo[2] is IsSystemParam, splitedinfo[3] is SystemParam and splitedinfo[4] is DisplayName
            //        /// </summary>
            //        var splitedinfo = progress.ProcedureInfo.Split(":");
            //        obj.ProgressStoredProcedureParamId = Convert.ToInt64(splitedinfo[0].ToString());
            //        obj.Name = splitedinfo[1];
            //        obj.DisplayName = splitedinfo[4];
            //        if (splitedinfo[2] == "0")
            //        {
            //            paramList.Add(obj);
            //        }
            //    }
            //}
            //response.RelatedProgresses.ToList().Clear();
            //response.RelatedProgresses = new();
            //foreach (var item in groupedResults)
            //{
            //    var obj = new GetRelatedProgressQueryResult
            //    {
            //        Params = paramList,
            //        Id = item.Items[0].Id,
            //        Name = item.Items[0].Name,
            //        ProgressId = item.ProgressId,
            //        TargetId = item.Items[0].TargetId,
            //        TargetName = item.Items[0].TargetName,
            //        //ProgressStoreProcedureId = item.ProgressStoreProcedureId,
            //    };
            //    response.RelatedProgresses.Add(obj);
            //}
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
}
