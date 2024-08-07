using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Resources;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;

public class LogisticRequestQueryRepository : ILogisticRequestQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    private readonly ISimaIdentity _simaIdentity;

    public LogisticRequestQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
					SELECT
						LR.Id
						,LR.Description
						,LR.CreatedAt
						,(p.FirstName + ' ' + P.LastName) CreatedBy
						,LR.ActiveStatusId
						,a.Name ActiveStatus
					FROM Logistics.LogisticsRequest LR
					INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
					INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
					INNER JOIN Basic.ActiveStatus A on A.ID = LR.ActiveStatusId
					
					
					---- GoodsLists
					
					SELECT 
						LRG.Id
						,LRG.Quantity
						,G.Code
						,G.Name
					FROM Logistics.LogisticsRequest LR
					INNER JOIN Logistics.LogisticsRequestGoods LRG ON LRG.LogisticsRequestId = LR.Id
					INNER JOIN Logistics.Goods G on G.Id = LRG.GoodsId
					
					
					------ IssueInformation

					select
						I.Id
						,I.Code
						,I.CurrentWorkflowId WorkflowId
						,W.Name WorkflowName
						,I.MainAggregateId
						,I.IssuePriorityId
						,IP.Name IssuePriorityName
						,I.IssueWeightCategoryId IssueWeightId
						,IWC.Name IssueWeightName
						,IWC.MaxRange Weight
						,I.CurrentStateId
						,State.Name CurrentStateName
						,I.CurrenStepId
						,State.Name CurrentStepName
						,i.DueDate
					FROM Logistics.LogisticsRequest LR
					INNER JOIN IssueManagement.Issue I ON I.SourceId = Lr.Id
					INNER JOIN Project.WorkFlow W ON I.CurrentWorkflowId = W.Id
					INNER JOIN IssueManagement.IssuePriority IP ON IP.Id = I.IssuePriorityId
					INNER JOIN IssueManagement.IssueWeightCategory IWC ON IWC.Id = I.IssueWeightCategoryId
					Left Join Project.State State on State.Id = i.CurrentStateId
					left Join Project.Step Step on Step.Id = i.CurrenStepId
					
					--------- IssueApproval

					select 
						SAO.Id StepApprovalOptionId
						,IAP.Description
						,Step2.Name StepName
						-- StepApprovalOptionName
						,Step2.Id StepId
						,WA.Id ActorId
						,WA.Name ActorName
						,(p2.FirstName + ' ' + P2.LastName) CreatedBy
						,(p3.FirstName + ' ' + P3.LastName) ApprovedBy
						,IAP.CreatedAt
					from Logistics.LogisticsRequest LR
					INNER JOIN IssueManagement.Issue I ON I.SourceId = Lr.Id
					INNER JOIN IssueManagement.IssueApproval IAP on IAP.IssueId = I.Id
					INNER join Project.StepApprovalOption SAO on SAO.ApprovalOptionId = IAP.Id
					INNER join Project.Step Step2 on Step2.Id = SAO.StepId
					INNER join Project.WorkFlowActorStep WAS on WAS.StepID = Step2.Id
					INNER join Project.WorkFlowActor WA on WA.Id = WAS.WorkFlowActorID
					INNER JOIN Authentication.Users U2 on LR.CreatedBy = U2.Id
					INNER JOIN Authentication.Profile P2 on P2.Id = U2.ProfileID
					INNER JOIN Authentication.Users U3 on IAP.ApprovedBy = U3.Id
					INNER JOIN Authentication.Profile P3 on P3.Id = U3.ProfileID
					
					
					
					------- PriceEstimation
					
					select
						PE.EstimationPrice
						,PE.Description
						,PE.CreatedAt
						,(p4.FirstName + ' ' + P4.LastName) CreatedBy
					FROM Logistics.LogisticsRequest LR
					INNER JOIN Logistics.PriceEstimation PE on PE.LogisticsRequestId = LR.Id
					INNER JOIN Authentication.Users U4 on LR.CreatedBy = U4.Id
					INNER JOIN Authentication.Profile P4 on P4.Id = U4.ProfileID
					
					
					
					---- CandidatedSupplier
					select
						CS.SupplierId
						,s.Name SupplierName
						,CS.IsSelected
						,CS.SelectionDate
						,RI.IsWrittenInquiry
						,RI.InquieredPrice
					FROM Logistics.LogisticsRequest LR
					INNER JOIN Logistics.CandidatedSupplier CS on CS.LogisticsRequestId = LR.Id
					inner join Logistics.Supplier S on CS.SupplierId = s.Id
					inner join Logistics.RequestInquiry RI on RI.LogisticsRequestId = LR.Id
					--inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RI.InvoiceDocumentId

					----- InvoiceDocument
					select 
						LRD2.Id
						,d.FileAddress DocumentPath
						,DT.Name DocumentTypeName
						,d.DocumentTypeId
						,d.FileExtensionId
						,DE.Name DocumentExtensionName
					FROM Logistics.LogisticsRequest LR
					inner join Logistics.RequestInquiry RI on RI.LogisticsRequestId = LR.Id
					inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RI.InvoiceDocumentId
					inner join DMS.Documents D on LRD2.DocumentId = d.Id
					inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
					inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
					
					
					------ Ordering
					select
						o.OrderDate
						,o.ReceiptNumber
						,o.Description
						,o.CreatedAt
						,o.IsContractRequired
						,o.IsPrePaymentRequired
						,(p.FirstName + ' ' + P.LastName) CreatedBy
					FROM Logistics.LogisticsRequest LR
					inner join Logistics.Ordering O on LR.Id = O.LogisticsRequestId
					INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
					INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
					
					
					------ ReceiptDocument
					select 
						LRD2.Id
						,d.FileAddress DocumentPath
						,DT.Name DocumentTypeName
						,d.DocumentTypeId
						,d.FileExtensionId
						,DE.Name DocumentExtensionName
					FROM Logistics.LogisticsRequest LR
					inner join Logistics.ReceiveOrder RO on RO.LogisticsRequestId = LR.Id
					inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RO.ReceiptDocumentId
					inner join DMS.Documents D on LRD2.DocumentId = d.Id
					inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
					inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
					
					-------- documents
					select 
						LRD2.Id
						,d.FileAddress DocumentPath
						,DT.Name DocumentTypeName
						,d.DocumentTypeId
						,d.FileExtensionId
						,DE.Name DocumentExtensionName
					FROM Logistics.LogisticsRequest LR
					inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.LogisticsRequestId = LR.Id
					inner join DMS.Documents D on LRD2.DocumentId = d.Id
					inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
					inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
					";
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> GetAll(GetAllLogisticsRequestsQuery request)
    {
        var userId = _simaIdentity.UserId;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = $@"  WITH Query as(
				SELECT   distinct
                      I.Id IssueId,
                      I.Code IssueCode,
                      L.Id,
                      L.Code,
                      I.[CurrentWorkflowId] WorkflowId,
                      W.Name WorkFlowName,
                      I.mainAggregateId,
                      IT.Name IssueTypeName,
                      IPP.Name IssuePriorityName,
                      I.weight,                            
                      ST.Name CurrentStateName,
                      S.Name CurrentStepName ,
                      I.summery IssueSummery,
                      I.description IssueDescription,
                      L.Description,
                      a.Name ActiveStatus,
                      P.[FirstName] as FirstName,
                      P.[LastName] as LastName,
                      L.[CreatedAt] 
                    from 
                    Logistics.LogisticsRequest L 
                    join IssueManagement.Issue I  on I.Id= L.IssueId and I.SourceId=L.Id AND I.MainAggregateId = 5
                    join Project.Step S on I.CurrenStepId = S.Id
                    join Project.WorkFlowActorStep WS on S.Id = WS.StepID
                    join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
                    left join Project.State ST on I.CurrentStateId = ST.Id
                    join Project.WorkFlow W on I.CurrentWorkflowId = W.Id
                    join Project.Project project on project.Id = W.ProjectID
                    left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
                    left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
                    join  [Authentication].[Users] U on L.CreatedBy = U.Id
                    join  [Authentication].[Profile] P on P.Id = U.ProfileID
                    join Basic.ActiveStatus a on L.ActiveStatusId=a.ID                       
                    WHERE L.CreatedBy = @userId and
                     I.ActiveStatusId != 3 and L.ActiveStatusId != 3 and W.ActiveStatusId != 3
                    )
                    SELECT Count(*) FROM Query
                     /**where**/
                     ; ";
            string query = $@" WITH Query as(
	               	SELECT  
distinct
                      I.Id IssueId,
                      I.Code IssueCode,
                      L.Id,
                      L.Code LogesticCode,
                      I.[CurrentWorkflowId] WorkflowId,
                      W.Name WorkFlowName,
                      I.mainAggregateId,
                      IT.Name IssueTypeName,
                      IPP.Name IssuePriorityName,
                      I.weight,                            
                      ST.Name CurrentStateName,
                      S.Name CurrentStepName ,
                      I.summery IssueSummery,
                      I.description IssueDescription,
                      L.Description LogesticDescription,
                      a.Name ActiveStatus,
                      P.[FirstName] as FirstName,
                      P.[LastName] as LastName,
                      L.[CreatedAt] 
                    from 
                    Logistics.LogisticsRequest L 
                    join IssueManagement.Issue I  on I.Id= L.IssueId and I.SourceId=L.Id AND I.MainAggregateId = 5
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
                    join  [Authentication].[Users] U on L.CreatedBy = U.Id
                    join  [Authentication].[Profile] P on P.Id = U.ProfileID
                    join Basic.ActiveStatus a on L.ActiveStatusId=a.ID                       
                    WHERE L.CreatedBy = @userId and
                     I.ActiveStatusId != 3 and L.ActiveStatusId != 3 and W.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("userId", userId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<LogisticCartablesGetAllQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetLogisticRequestsQueryResult> GetById(GetLogisticRequestsQuery request)
    {
        try
        {
            var response = new GetLogisticRequestsQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
        SELECT
        	LR.Id
        	,LR.Description
        	,LR.CreatedAt
        	,(p.FirstName + ' ' + P.LastName) CreatedBy
        	,LR.ActiveStatusId
        	,a.Name ActiveStatus
        FROM Logistics.LogisticsRequest LR
        INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
        INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
        INNER JOIN Basic.ActiveStatus A on A.ID = LR.ActiveStatusId
        WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


        			---- GoodsLists

        			SELECT 
        				LRG.Id
        				,LRG.Quantity
        				,G.Code
        				,G.Name
        			FROM Logistics.LogisticsRequest LR
        			INNER JOIN Logistics.LogisticsRequestGoods LRG ON LRG.LogisticsRequestId = LR.Id
        			INNER JOIN Logistics.Goods G on G.Id = LRG.GoodsId
        			WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

        			------ IssueInformation
        			select
        				I.Id
        				,I.Code
        				,I.CurrentWorkflowId WorkflowId
        				,W.Name WorkflowName
        				,I.MainAggregateId
        				,I.IssuePriorityId
        				,IP.Name IssuePriorityName
        				,I.IssueWeightCategoryId issueWeightId
        				,IWC.Name issueWeightName
        				,IWC.MaxRange Weight
        				,I.CurrentStateId
        				,State.Name CurrentStateName
        				,I.CurrenStepId
        				,State.Name CurrentStepName
        				,i.DueDate
        			FROM Logistics.LogisticsRequest LR
        			INNER JOIN IssueManagement.Issue I ON I.SourceId = Lr.Id
        			INNER JOIN Project.WorkFlow W ON I.CurrentWorkflowId = W.Id
        			INNER JOIN IssueManagement.IssuePriority IP ON IP.Id = I.IssuePriorityId
        			INNER JOIN IssueManagement.IssueWeightCategory IWC ON IWC.Id = I.IssueWeightCategoryId
        			Left Join Project.State State on State.Id = i.CurrentStateId
        			left Join Project.Step Step on Step.Id = i.CurrenStepId
        			WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

        						--------- IssueApproval
        						select 
        							SAO.Id StepApprovalOptionId
        							,IAP.Description
        							,Step2.Name StepName
        							-- StepApprovalOptionName
        							,Step2.Id StepId
        							,WA.Id ActorId
        							,WA.Name ActorName
        							,(p2.FirstName + ' ' + P2.LastName) CreatedBy
        							,(p3.FirstName + ' ' + P3.LastName) ApprovedBy
        							,IAP.CreatedAt
        						from Logistics.LogisticsRequest LR
        						INNER JOIN IssueManagement.Issue I ON I.SourceId = Lr.Id
        						INNER JOIN IssueManagement.IssueApproval IAP on IAP.IssueId = I.Id
        						INNER join Project.StepApprovalOption SAO on SAO.ApprovalOptionId = IAP.Id
        						INNER join Project.Step Step2 on Step2.Id = SAO.StepId
        						INNER join Project.WorkFlowActorStep WAS on WAS.StepID = Step2.Id
        						INNER join Project.WorkFlowActor WA on WA.Id = WAS.WorkFlowActorID
        						INNER JOIN Authentication.Users U2 on LR.CreatedBy = U2.Id
        						INNER JOIN Authentication.Profile P2 on P2.Id = U2.ProfileID
        						INNER JOIN Authentication.Users U3 on IAP.ApprovedBy = U3.Id
        						INNER JOIN Authentication.Profile P3 on P3.Id = U3.ProfileID
        						WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


        			------- PriceEstimation

        			select
        				PE.EstimationPrice
        				,PE.Description
        				,PE.CreatedAt
        				,(p4.FirstName + ' ' + P4.LastName) CreatedBy
        			FROM Logistics.LogisticsRequest LR
        			INNER JOIN Logistics.PriceEstimation PE on PE.LogisticsRequestId = LR.Id
        			INNER JOIN Authentication.Users U4 on LR.CreatedBy = U4.Id
        			INNER JOIN Authentication.Profile P4 on P4.Id = U4.ProfileID
        			WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


        			---- CandidatedSupplier
        			select
        				CS.SupplierId
        				,s.Name SupplierName
        				,CS.IsSelected
        				,CS.SelectionDate
        				,RI.IsWrittenInquiry
        				,RI.InquieredPrice
        			FROM Logistics.LogisticsRequest LR
        			INNER JOIN Logistics.CandidatedSupplier CS on CS.LogisticsRequestId = LR.Id
        			inner join Logistics.Supplier S on CS.SupplierId = s.Id
        			inner join Logistics.RequestInquiry RI on RI.LogisticsRequestId = LR.Id
        			WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

        						----- InvoiceDocument
        						select 
        							LRD2.Id
        							,d.FileAddress DocumentPath
        							,DT.Name DocumentTypeName
        							,d.DocumentTypeId
        							,d.FileExtensionId
        							,DE.Name DocumentExtensionName
        						FROM Logistics.LogisticsRequest LR
        						inner join Logistics.RequestInquiry RI on RI.LogisticsRequestId = LR.Id
        						inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RI.InvoiceDocumentId
        						inner join DMS.Documents D on LRD2.DocumentId = d.Id
        						inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
        						inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
        						WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

        			------ Ordering
        			select
        				o.OrderDate
        				,o.ReceiptNumber
        				,o.Description
        				,o.CreatedAt
        				,o.IsContractRequired
        				,o.IsPrePaymentRequired
        				,(p.FirstName + ' ' + P.LastName) CreatedBy
        			FROM Logistics.LogisticsRequest LR
        			inner join Logistics.Ordering O on LR.Id = O.LogisticsRequestId
        			INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
        			INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
        			WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

        						------ ReceiptDocument
        						select 
        							LRD2.Id
        							,d.FileAddress DocumentPath
        							,DT.Name DocumentTypeName
        							,d.DocumentTypeId
        							,d.FileExtensionId
        							,DE.Name DocumentExtensionName
        						FROM Logistics.LogisticsRequest LR
        						inner join Logistics.ReceiveOrder RO on RO.LogisticsRequestId = LR.Id
        						inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RO.ReceiptDocumentId
        						inner join DMS.Documents D on LRD2.DocumentId = d.Id
        						inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
        						inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
        						WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

        			-------- documents
        			select 
        				LRD2.Id
        				,d.FileAddress DocumentPath
        				,DT.Name DocumentTypeName
        				,d.DocumentTypeId
        				,d.FileExtensionId
        				,DE.Name DocumentExtensionName
        			FROM Logistics.LogisticsRequest LR
        			inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.LogisticsRequestId = LR.Id
        			inner join DMS.Documents D on LRD2.DocumentId = d.Id
        			inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
        			inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
        			WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

        ";



                using (var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id }))
                {
                    response = multi.ReadAsync<GetLogisticRequestsQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                    response.GoodsList = multi.ReadAsync<GoodsList>().GetAwaiter().GetResult().ToList();
                    response.IssueInfo = multi.ReadAsync<IssueInfo>().GetAwaiter().GetResult().FirstOrDefault();
                    response.IssueApprovalList = multi.ReadAsync<IssueApprovalList>().GetAwaiter().GetResult().ToList();
                    response.PriceEstimationInfo = multi.ReadAsync<PriceEstimationList>().GetAwaiter().GetResult().FirstOrDefault();
                    response.CandidateSupplierList = multi.ReadAsync<CandidatedSupplierList>().GetAwaiter().GetResult().ToList();
                    response.InvoiceDocumentList = multi.ReadAsync<InvoiceDocumentList>().GetAwaiter().GetResult().ToList();
                    response.OrderingInfo = multi.ReadAsync<OrderingList>().GetAwaiter().GetResult().FirstOrDefault();
                    response.ReceiptDocumentList = multi.ReadAsync<ReceiptDocumentList>().GetAwaiter().GetResult().ToList();
                    response.DocumentList = multi.ReadAsync<DocumentList>().GetAwaiter().GetResult().ToList();
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

    public async Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> GetLogesticCartables(LogisticCartableGetAllQuery request)
    {
        var userId = _simaIdentity.UserId;
        var roleIds = _simaIdentity.RoleIds;
        var groupIds = _simaIdentity.GroupId;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = $@"  WITH Query as(
				SELECT  distinct
                      I.Id IssueId,
                      I.Code IssueCode,
                      L.Id,
                      L.Code,
                      I.[CurrentWorkflowId] WorkflowId,
                      W.Name WorkFlowName,
                      I.mainAggregateId,
                      IT.Name IssueTypeName,
                      IPP.Name IssuePriorityName,
                      I.weight,                            
                      ST.Name CurrentStateName,
                      S.Name CurrentStepName ,
                      I.summery IssueSummery,
                      I.description IssueDescription,
                      L.Description,
                      a.Name ActiveStatus,
                      P.[FirstName] as FirstName,
                      P.[LastName] as LastName,
                      L.[CreatedAt] 
                    from 
                    Logistics.LogisticsRequest L 
                    join IssueManagement.Issue I  on I.Id= L.IssueId and I.SourceId=L.Id AND I.MainAggregateId = 5
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
                    join Basic.ActiveStatus a on L.ActiveStatusId=a.ID                       

                    WHERE (WR.RoleID IN @roleIds or WG.GroupID IN @groupIds or  WU.UserID=@userId) and
                     I.ActiveStatusId != 3 and L.ActiveStatusId != 3 and W.ActiveStatusId != 3
                    )
                    SELECT Count(*) FROM Query
                     /**where**/
                     ; ";
            string query = $@" WITH Query as(
	               	SELECT  distinct
                      I.Id IssueId,
                      I.Code IssueCode,
                      L.Id,
                      L.Code LogesticCode,
                      I.[CurrentWorkflowId] WorkflowId,
                      W.Name WorkFlowName,
                      I.mainAggregateId,
                      IT.Name IssueTypeName,
                      IPP.Name IssuePriorityName,
                      I.weight,                            
                      ST.Name CurrentStateName,
                      S.Name CurrentStepName ,
                      I.summery IssueSummery,
                      I.description IssueDescription,
                      L.Description LogesticDescription,
                      a.Name ActiveStatus,
                      P.[FirstName] as FirstName,
                      P.[LastName] as LastName,
                      L.[CreatedAt] 
                    from 
                    Logistics.LogisticsRequest L 
                    join IssueManagement.Issue I  on I.Id= L.IssueId and I.SourceId=L.Id AND I.MainAggregateId = 5
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
                    join Basic.ActiveStatus a on L.ActiveStatusId=a.ID                     

                    WHERE (WR.RoleID IN @roleIds or WG.GroupID IN @groupIds or  WU.UserID=@userId) and
                     I.ActiveStatusId != 3 and L.ActiveStatusId != 3 and W.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("userId", userId);
            dynaimcParameters.Item2.Add("roleIds", roleIds);
            dynaimcParameters.Item2.Add("groupIds", groupIds);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<LogisticCartablesGetAllQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<LogisticCartableGetQueryResult> GetLogesticCartableDetail(long logesticId, long issueId)
    {
        try
        {
            var response = new LogisticCartableGetQueryResult();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
 SELECT
     LR.Id
     ,LR.Description
     ,LR.CreatedAt
     ,(p.FirstName + ' ' + P.LastName) CreatedBy
     ,LR.ActiveStatusId
     ,a.Name ActiveStatus
  ,D.Name CreatorDepartmentName
  ,d.Id CreatorDepartmentId
  ,st.UIPropertyBoxTitle
  ,case when LR.CreatedBy = @userId then '1' else '0' end IsEditable
FROM Logistics.LogisticsRequest LR
inner join IssueManagement.Issue I on i.SourceId = lr.Id
inner join Project.Step st on st.Id = i.CurrenStepId
INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
INNER JOIN Basic.ActiveStatus A on A.ID = LR.ActiveStatusId
inner join Organization.Staff S on S.ProfileId = P.Id
inner join Organization.Position PO on po.Id = s.PositionId
inner join Organization.Department D on d.Id = Po.DepartmentId
  WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


 ---- GoodsLists

 SELECT 
  LRG.Id
  ,LRG.Quantity
  ,G.Code
  ,G.Name
  ,GC.Id GoodsCategoryId
  ,GC.Name GoodsCategoryName
  ,GT.Id GoodsTypeId
  ,GT.Name GoodsTypeName
  ,UM.Id UnitMeasurementId
  ,UM.Name UnitMeasurementName
  ,GC.IsRequiredSecurityCheck
  ,G.IsFixedAsset
FROM Logistics.LogisticsRequest LR
INNER JOIN Logistics.LogisticsRequestGoods LRG ON LRG.LogisticsRequestId = LR.Id
INNER JOIN Logistics.Goods G on G.Id = LRG.GoodsId
inner join Logistics.UnitMeasurement UM on UM.Id = G.UnitMeasurementId
inner join Logistics.GoodsCategory GC on gc.Id = g.GoodsCategoryId
inner join Logistics.GoodsType GT on gt.Id =gc.GoodsTypeId
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

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
 FROM Logistics.LogisticsRequest LR
 INNER JOIN IssueManagement.Issue I ON I.Id = Lr.IssueId
 INNER JOIN Project.WorkFlow W ON I.CurrentWorkflowId = W.Id
 INNER JOIN IssueManagement.IssuePriority IP ON IP.Id = I.IssuePriorityId
 INNER JOIN IssueManagement.IssueWeightCategory IWC ON IWC.Id = I.IssueWeightCategoryId
 Left Join Project.State State on State.Id = i.CurrentStateId
 left Join Project.Step Step on Step.Id = i.CurrenStepId
 WHERE LR.Id = @Id and LR.IssueId = @issueId AND LR.ActiveStatusId <> 3

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
Where I.ActiveStatusId != 3 and I.Id = @issueId
order by IC.CreatedAt desc

             --------- IssueApproval
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
             from Logistics.LogisticsRequest LR
             INNER JOIN IssueManagement.Issue I ON I.SourceId = Lr.Id
             INNER JOIN IssueManagement.IssueApproval IAP on IAP.IssueId = I.Id
             INNER join Project.StepApprovalOption SAO on SAO.ApprovalOptionId = IAP.Id
             INNER join Project.ApprovalOption AO on AO.Id = SAO.ApprovalOptionId
             INNER join Project.Step Step2 on Step2.Id = SAO.StepId
             INNER join Project.WorkFlowActorStep WAS on WAS.StepID = Step2.Id
             INNER join Project.WorkFlowActor WA on WA.Id = WAS.WorkFlowActorID
             INNER JOIN Authentication.Users U2 on LR.CreatedBy = U2.Id
             INNER JOIN Authentication.Profile P2 on P2.Id = U2.ProfileID
             INNER JOIN Authentication.Users U3 on IAP.ApprovedBy = U3.Id
             INNER JOIN Authentication.Profile P3 on P3.Id = U3.ProfileID
             WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


 ------- PriceEstimation

 select
   PE.EstimationPrice
   ,PE.Description
   ,PE.CreatedAt
   ,(p4.FirstName + ' ' + P4.LastName) CreatedBy
 FROM Logistics.LogisticsRequest LR
 INNER JOIN Logistics.PriceEstimation PE on PE.LogisticsRequestId = LR.Id
 INNER JOIN Authentication.Users U4 on PE.CreatedBy = U4.Id
 INNER JOIN Authentication.Profile P4 on P4.Id = U4.ProfileID
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3






 ---- CandidatedSupplier
  select distinct
  CS.SupplierId
  ,s.Name SupplierName
  ,CS.IsSelected
  ,CS.SelectionDate
  ,RI.IsWrittenInquiry
  ,RI.InquieredPrice
  ,D.Id InvoiceDocumentId
  ,D.FileAddress InvoiceDocumentPath
  ,CS.CreatedAt 
  ,(p4.FirstName + ' ' + P4.LastName) CreatedBy
FROM Logistics.LogisticsRequest LR
INNER JOIN Logistics.CandidatedSupplier CS on CS.LogisticsRequestId = LR.Id
inner join Logistics.Supplier S on CS.SupplierId = s.Id
inner join Logistics.RequestInquiry RI on RI.CandidatedSupplierId = CS.Id
Left Join DMS.Documents D on D.Id = RI.InvoiceDocumentId
INNER JOIN Authentication.Users U4 on CS.CreatedBy = U4.Id
INNER JOIN Authentication.Profile P4 on P4.Id = U4.ProfileID
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3
----- InvoiceDocument
             select 
                  LRD2.Id
                  ,d.FileAddress DocumentPath
                  ,DT.Name DocumentTypeName
                  ,d.DocumentTypeId
                  ,d.FileExtensionId
                  ,DE.Name DocumentExtensionName
                  ,S.Name AttachStepName
             FROM Logistics.LogisticsRequest LR
             inner join Logistics.RequestInquiry RI on RI.LogisticsRequestId = LR.Id
             inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RI.InvoiceDocumentId
             inner join DMS.Documents D on LRD2.DocumentId = d.Id
             inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
             inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
             left join Project.Step S on S.Id = D.AttachStepId
             WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

 ------ Ordering
 select
   o.OrderDate
   ,o.ReceiptNumber
   ,o.Description
   ,o.CreatedAt
   ,o.IsContractRequired
   ,o.IsPrePaymentRequired
   ,(p.FirstName + ' ' + P.LastName) CreatedBy
 FROM Logistics.LogisticsRequest LR
 inner join Logistics.Ordering O on LR.Id = O.LogisticsRequestId
 INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
 INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


  -------- supplierContract
  select
  SC.Id
  ,SC.ContractNumber
  ,SC.ContractDate 
  ,SC.ContractDocumentId 
  ,SC.Description 
  from Logistics.LogisticsRequest LR
  Inner Join Logistics.SupplierContract SC on LR.Id = SC.LogisticsRequestId
  where LR.Id = @Id



  ------- paymentCommandInfo
  select
  pc.Id
  ,pc.CommandDate
  ,pc.CreatedAt
  ,pc.CommandDescription
  ,(P.FirstName + ' ' + P.LastName) CreatedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentCommand Pc on pc.LogisticsRequestId = LR.Id
  INNER JOIN Authentication.Users U on Pc.CreatedBy = U.Id
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
  where LR.Id = @Id and pc.IsPrePayment <> '1'


  -------- paymentHistoryInfo
  select
  PH.PaymentCommandId
  ,ph.PaymentDate
  ,ph.PaymentNumber
  ,ph.PaymentTypeId
  ,ph.PaymentValue
  ,pt.Name PaymentTypeName
  ,ph.Description PaymentDescription
  ,ph.PaymentDocumentId
  ,(P.FirstName + ' ' + P.LastName) CreatedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentHistory PH on Ph.LogisticsRequestId = LR.Id
  inner join Bank.PaymentType pt on pt.Id = PH.PaymentTypeId
  INNER JOIN Authentication.Users U on PH.CreatedBy = U.Id
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
  where LR.Id = @Id and ph.IsPrePayment <> '1'


------- prepaymentCommandInfo
select
  pc.Id
  ,pc.CommandDate
  ,pc.CreatedAt
  ,pc.CommandDescription
 ,(P.FirstName + ' ' + P.LastName) CreatedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentCommand Pc on pc.LogisticsRequestId = LR.Id
  INNER JOIN Authentication.Users U on Pc.CreatedBy = U.Id
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
  where LR.Id = @Id and pc.IsPrePayment = '1'

  ---------prepaymentHistoryInfo
  select
  PH.PaymentCommandId
  ,ph.PaymentDate
  ,ph.PaymentNumber
  ,ph.PaymentValue
  ,ph.PaymentTypeId
  ,pt.Name PaymentTypeName
  ,ph.Description PaymentDescription
  ,ph.PaymentDocumentId
 ,(P.FirstName + ' ' + P.LastName) CreatedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentHistory PH on Ph.LogisticsRequestId = LR.Id
  inner join Bank.PaymentType pt on pt.Id = PH.PaymentTypeId
  INNER JOIN Authentication.Users U on PH.CreatedBy = U.Id
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
  where LR.Id = @Id and ph.IsPrePayment = '1'


  -------- receiveInfo
  select 
  RO.ReceiveDate
  ,RO.ReceiptNumber
  ,RO.ReceiptDocumentId
  ,RO.Description
  ,(p.FirstName + ' ' + p.LastName) ReceivedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.ReceiveOrder RO on LR.Id = RO.LogisticsRequestId
  inner join Authentication.Users u on u.Id = RO.CreatedBy
  inner join Authentication.Profile p on p.Id = u.ProfileID
  where LR.Id = @Id




  -------- deliveryInfo
  select
  DO.DeliveryDate
  ,Do.ReceiptDocumentId
  ,do.Description
  ,(p.FirstName + ' ' + p.LastName) DeliveredBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.DeliveryOrder DO on LR.Id = DO.LogisticsRequestId
  inner join Authentication.Users u on u.Id = DO.CreatedBy
  inner join Authentication.Profile p on p.Id = u.ProfileID
  where LR.Id = @Id


  ------- returnInfo
  select
  DO.ReturnDate
  ,do.Description
  ,(p.FirstName + ' ' + p.LastName) ReturnedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.ReturnOrder DO on LR.Id = DO.LogisticsRequestId
  inner join Authentication.Users u on u.Id = DO.CreatedBy
  inner join Authentication.Profile p on p.Id = u.ProfileID
  where LR.Id = @Id




             ------ ReceiptDocument
             select 
                  LRD2.Id
                  ,d.FileAddress DocumentPath
                  ,DT.Name DocumentTypeName
                  ,d.DocumentTypeId
                  ,d.FileExtensionId
                  ,DE.Name DocumentExtensionName
                  ,S.Name AttachStepName
             FROM Logistics.LogisticsRequest LR
             inner join Logistics.ReceiveOrder RO on RO.LogisticsRequestId = LR.Id
             inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RO.ReceiptDocumentId
             inner join DMS.Documents D on LRD2.DocumentId = d.Id
             inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
             inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
             left join Project.Step S on S.Id = D.AttachStepId
             WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


---- goodsCoding
			select
gc.GoodsId
,g.Name GoodsName
,gc.Code
,GCA.Id GoodsCategoryId
,GCA.Name GoodsCategoryName
,GT.Id GoodsTypeId
,GT.Name GoodsTypeName
FROM Logistics.LogisticsRequest LR
INNER JOIN Logistics.LogisticsRequestGoods LRG ON LRG.LogisticsRequestId = LR.Id
INNER JOIN Logistics.Goods G on G.Id = LRG.GoodsId
inner join Logistics.GoodsCoding gc on gc.GoodsId = G.Id
left join Logistics.GoodsCategory GCA on GCA.Id = G.GoodsCategoryId
left join Logistics.GoodsType GT on GCA.GoodsTypeId= GT.Id
WHERE LR.Id = @Id

 -------- documents
 select 
   D.Id
   ,d.FileAddress DocumentPath
   ,d.Name DocumentFileName
   ,DT.Name DocumentTypeName
   ,d.DocumentTypeId
   ,d.FileExtensionId
   ,DE.Name DocumentExtensionName
   ,(p.FirstName + ' ' + p.LastName) CreatedBy
   ,LRD2.CreatedAt
   ,S.Name AttachStepName
 FROM Logistics.LogisticsRequest LR
 inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.LogisticsRequestId = LR.Id
 inner join DMS.Documents D on LRD2.DocumentId = d.Id
 inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
 inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
 left join Authentication.Users u on u.Id = LRD2.CreatedBy
 left join Authentication.Profile p on p.Id = u.ProfileID
 left join Project.Step S on S.Id = D.AttachStepId
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


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
        Where    I.Id = @issueId  and  (WU.UserID=@userId or WR.RoleId in @RoleIds or WG.GroupId in @GroupIds)
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
                ,PSPP.[TextBoundName]
                ,PSPP.[ValueBoundName]
				,AMA.Name ApiMethodAction
                From  
                        [Project].ProgressStoreProcedure PSP 
                        inner join Project.ProgressStoreProcedureParam PSPP on PSP.Id = pspp.ProgressStoreProcedureId
                        inner join Basic.UIInputElement  UI on UI.Id = pspp.UiInputElementId 
						left join Basic.ApiMethodAction AMA on AMA.Id = PSPP.ApiMethodActionId

                		where Psp.ProgressId In
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

             -- `StepRequiredDocument

               SELECT 
                    dt.Id DocumentTypeId,
                    dt.Name DocumentTypeName,
                    srd.Count
                FROM  [IssueManagement].[Issue] I
                JOIN  [Project].[Step] S on I.CurrenStepId = S.Id
                left join Project.StepRequiredDocument srd on srd.StepId = s.Id
                left join DMS.DocumentType dt on dt.Id =srd.DocumentTypeId
              Where I.ActiveStatusId != 3 and I.Id =  @issueId and srd.ActiveStatusID <>3

             --ApprovalOption

             EXEC [Project].[ReturnApprovalList] @issueId";


                using (var multi = await connection.QueryMultipleAsync(query, new { Id = logesticId, issueId = issueId, userId = _simaIdentity.UserId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId }))
                {
                    response = multi.ReadAsync<LogisticCartableGetQueryResult>().GetAwaiter().GetResult()?.FirstOrDefault();
                    if (response is null) throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);

                    response.GoodsList = multi.ReadAsync<GoodsList>().GetAwaiter().GetResult()?.ToList();
                    response.IssueInfo = multi.ReadAsync<IssueInfo>().GetAwaiter().GetResult()?.FirstOrDefault();
                    if (response.IssueInfo is not null)
                        response.IssueInfo.IssueCommentList = await multi.ReadAsync<GetIssueCommentQueryResult>();
                    response.IssueApprovalList = (await multi.ReadAsync<IssueApprovalList>())?.ToList();
                    response.PriceEstimationInfo = (await multi.ReadAsync<PriceEstimationList>())?.FirstOrDefault();
                    response.CandidateSupplierList = (await multi.ReadAsync<CandidatedSupplierList>())?.ToList();
                    response.InvoiceDocumentList = (await multi.ReadAsync<InvoiceDocumentList>())?.ToList();
                    response.OrderingInfo = (await multi.ReadAsync<OrderingList>())?.FirstOrDefault();
                    response.SupplierContractInfo = (await multi.ReadAsync<SupplierContractInfo>())?.FirstOrDefault();
                    response.PaymentCommandInfo = (await multi.ReadAsync<PaymentCommandInfo>())?.FirstOrDefault();
                    response.PaymentHistoryInfo = (await multi.ReadAsync<PaymentHistoryInfo>())?.FirstOrDefault();
                    response.PrePaymentCommandInfo = (await multi.ReadAsync<PaymentCommandInfo>())?.FirstOrDefault();
                    response.PrePaymentHistoryInfo = (await multi.ReadAsync<PaymentHistoryInfo>())?.FirstOrDefault();
                    response.ReceiveInfo = (await multi.ReadAsync<ReceiveInfo>())?.FirstOrDefault();
                    response.DeliveryInfo = (await multi.ReadAsync<DeliveryInfo>())?.FirstOrDefault();
                    response.ReturnInfo = (await multi.ReadAsync<ReturnInfo>())?.FirstOrDefault();
                    response.ReceiptDocumentList = (await multi.ReadAsync<ReceiptDocumentList>())?.ToList();
                    response.GoodsCodingList = (await multi.ReadAsync<GoodsCoding>())?.ToList();
                    response.DocumentList = (await multi.ReadAsync<DocumentList>())?.ToList();
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
                            ApiNameForDataBounding = x.First().ApiNameForDataBounding?.Replace("{Id}", logesticId.ToString()),
                            ApiMethodAction = x.First().ApiMethodAction,
                            DisplayName = x.First().DisplayName,
                            BoundFormat = x.First().BoundFormat,
                            HasInputInEachRecord = x.First().HasInputInEachRecord,
                            IsMultiSelect = x.First().IsMultiSelect,
                            IsSingleSelect = x.First().IsSingleSelect,
                            JsonFormat = x.First().JsonFormat,
                            ProgressId = x.First().ProgressId,
                            UiInputElementId = x.First().UiInputElementId,
                            UiInputElementName = x.First().UiInputElementName,
                            TextBoundName = x.First().TextBoundName,
                            ValueBoundName = x.First().ValueBoundName,
                            DeserializedBoundFormat = !string.IsNullOrEmpty(x.First().BoundFormat) ? JsonConvert.DeserializeObject<IEnumerable<BoundFormatDeserialized>>(x.First().BoundFormat) : null
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
                                        param.DeserializedBoundFormat = JsonConvert.DeserializeObject<IEnumerable<BoundFormatDeserialized>>(param.BoundFormat);
                                }
                            }
                        }
                    }

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

    public async Task<Result<IEnumerable<GetLogesticRequestGoodsQueryResult>>> GetLogesticRequestGoods(long Id)
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = $@" select 
                                RG.LogisticsRequestId,
                                RG.GoodsId,
                                RG.Quantity,
                                G.Name GoodsName,
                                G.Code GoodsCode,
                                GC.Name GoodsCategoryName
                                from Logistics.LogisticsRequestGoods RG
                                join Logistics.LogisticsRequest LR on RG.LogisticsRequestId = LR.Id
                                join Logistics.Goods G on  G.Id = RG.GoodsId 
                                join Logistics.GoodsCategory GC on GC.Id = G.GoodsCategoryId
                                where LR.Id = @LogesticRequestId and LR.ActiveStatusId != 3 and G.ActiveStatusId != 3 and RG.ActiveStatusId != 3";
            using (var multi = await connection.QueryMultipleAsync(query, new { LogesticRequestId = Id }))
            {
                var response = await multi.ReadAsync<GetLogesticRequestGoodsQueryResult>();
                if (response is null) throw new SimaResultException(CodeMessges._400Code, Messages.GoodsIsNull);

                var result = new List<GetLogesticRequestGoodsQueryResult>();
                int id = 0;
                foreach (var item in response)
                {
                    for (int i = 1; i <= item.Quantity; i++)
                    {
                        id++;
                        var goods = new GetLogesticRequestGoodsQueryResult
                        {
                            Id = id,
                            GoodsCode = item.GoodsCode,
                            GoodsCategoryName = item.GoodsCategoryName,
                            GoodsId = item.GoodsId,
                            GoodsName = item.GoodsName,
                            LogisticsRequestId = item.LogisticsRequestId,
                            Quantity = 1
                        };
                        result.Add(goods);
                    }
                }

                return Result.Ok(result.AsEnumerable());
            }
        }
    }
    /// <summary>
    /// دریافت اطلاعات یک درخواست دارکات و خرید برای ثبت کننده آن
    /// </summary>
    /// <param name="logesticId"></param>
    /// <param name="issueId"></param>
    /// <returns></returns>
    public async Task<GetMyLogisticCartableGetQueryResult> GetMyLogesticCartableDetail(long logesticId, long issueId)
    {
        var response = new GetMyLogisticCartableGetQueryResult();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
 SELECT
     LR.Id
     ,LR.Description
     ,LR.CreatedAt
     ,(p.FirstName + ' ' + P.LastName) CreatedBy
     ,LR.ActiveStatusId
     ,a.Name ActiveStatus
  ,D.Name CreatorDepartmentName
  ,d.Id CreatorDepartmentId
  ,st.UIPropertyBoxTitle
  ,1 IsEditable
FROM Logistics.LogisticsRequest LR
inner join IssueManagement.Issue I on i.SourceId = lr.Id
inner join Project.Step st on st.Id = i.CurrenStepId
INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
INNER JOIN Basic.ActiveStatus A on A.ID = LR.ActiveStatusId
inner join Organization.Staff S on S.ProfileId = P.Id
inner join Organization.Position PO on po.Id = s.PositionId
inner join Organization.Department D on d.Id = Po.DepartmentId
  WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId


 ---- GoodsLists

 SELECT 
  LRG.Id
  ,LRG.Quantity
  ,G.Code
  ,G.Name
  ,GC.Id GoodsCategoryId
  ,GC.Name GoodsCategoryName
  ,GT.Id GoodsTypeId
  ,GT.Name GoodsTypeName
  ,UM.Id UnitMeasurementId
  ,UM.Name UnitMeasurementName
  ,GC.IsRequiredSecurityCheck
  ,G.IsFixedAsset
FROM Logistics.LogisticsRequest LR
INNER JOIN Logistics.LogisticsRequestGoods LRG ON LRG.LogisticsRequestId = LR.Id
INNER JOIN Logistics.Goods G on G.Id = LRG.GoodsId
inner join Logistics.UnitMeasurement UM on UM.Id = G.UnitMeasurementId
inner join Logistics.GoodsCategory GC on gc.Id = g.GoodsCategoryId
inner join Logistics.GoodsType GT on gt.Id =gc.GoodsTypeId
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId

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
 FROM Logistics.LogisticsRequest LR
 INNER JOIN IssueManagement.Issue I ON I.Id = Lr.IssueId
 INNER JOIN Project.WorkFlow W ON I.CurrentWorkflowId = W.Id
 INNER JOIN IssueManagement.IssuePriority IP ON IP.Id = I.IssuePriorityId
 INNER JOIN IssueManagement.IssueWeightCategory IWC ON IWC.Id = I.IssueWeightCategoryId
 Left Join Project.State State on State.Id = i.CurrentStateId
 left Join Project.Step Step on Step.Id = i.CurrenStepId
 WHERE LR.Id = @Id and LR.IssueId = @issueId AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId

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
Where I.ActiveStatusId != 3 and I.Id = @issueId
order by IC.CreatedAt desc

             --------- IssueApproval
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
             from Logistics.LogisticsRequest LR
             INNER JOIN IssueManagement.Issue I ON I.SourceId = Lr.Id
             INNER JOIN IssueManagement.IssueApproval IAP on IAP.IssueId = I.Id
             INNER join Project.StepApprovalOption SAO on SAO.ApprovalOptionId = IAP.Id
             INNER join Project.ApprovalOption AO on AO.Id = SAO.ApprovalOptionId
             INNER join Project.Step Step2 on Step2.Id = SAO.StepId
             INNER join Project.WorkFlowActorStep WAS on WAS.StepID = Step2.Id
             INNER join Project.WorkFlowActor WA on WA.Id = WAS.WorkFlowActorID
             INNER JOIN Authentication.Users U2 on LR.CreatedBy = U2.Id
             INNER JOIN Authentication.Profile P2 on P2.Id = U2.ProfileID
             INNER JOIN Authentication.Users U3 on IAP.ApprovedBy = U3.Id
             INNER JOIN Authentication.Profile P3 on P3.Id = U3.ProfileID
             WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId


 ------- PriceEstimation

 select
   PE.EstimationPrice
   ,PE.Description
   ,PE.CreatedAt
   ,(p4.FirstName + ' ' + P4.LastName) CreatedBy
 FROM Logistics.LogisticsRequest LR
 INNER JOIN Logistics.PriceEstimation PE on PE.LogisticsRequestId = LR.Id
 INNER JOIN Authentication.Users U4 on PE.CreatedBy = U4.Id
 INNER JOIN Authentication.Profile P4 on P4.Id = U4.ProfileID
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId






 ---- CandidatedSupplier
  select distinct
  CS.SupplierId
  ,s.Name SupplierName
  ,CS.IsSelected
  ,CS.SelectionDate
  ,RI.IsWrittenInquiry
  ,RI.InquieredPrice
  ,D.Id InvoiceDocumentId
  ,D.FileAddress InvoiceDocumentPath
  ,CS.CreatedAt 
  ,(p4.FirstName + ' ' + P4.LastName) CreatedBy
FROM Logistics.LogisticsRequest LR
INNER JOIN Logistics.CandidatedSupplier CS on CS.LogisticsRequestId = LR.Id
inner join Logistics.Supplier S on CS.SupplierId = s.Id
inner join Logistics.RequestInquiry RI on RI.CandidatedSupplierId = CS.Id
Left Join DMS.Documents D on D.Id = RI.InvoiceDocumentId
INNER JOIN Authentication.Users U4 on CS.CreatedBy = U4.Id
INNER JOIN Authentication.Profile P4 on P4.Id = U4.ProfileID
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId
----- InvoiceDocument
             select 
                  LRD2.Id
                  ,d.FileAddress DocumentPath
                  ,DT.Name DocumentTypeName
                  ,d.DocumentTypeId
                  ,d.FileExtensionId
                  ,DE.Name DocumentExtensionName
,S.Name AttachStepName
             FROM Logistics.LogisticsRequest LR
             inner join Logistics.RequestInquiry RI on RI.LogisticsRequestId = LR.Id
             inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RI.InvoiceDocumentId
             inner join DMS.Documents D on LRD2.DocumentId = d.Id
             inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
             inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
left join Project.Step S on S.Id = D.AttachStepId
             WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId

 ------ Ordering
 select
   o.OrderDate
   ,o.ReceiptNumber
   ,o.Description
   ,o.CreatedAt
   ,o.IsContractRequired
   ,o.IsPrePaymentRequired
   ,(p.FirstName + ' ' + P.LastName) CreatedBy
 FROM Logistics.LogisticsRequest LR
 inner join Logistics.Ordering O on LR.Id = O.LogisticsRequestId
 INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
 INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId


  -------- supplierContract
  select
  SC.Id
  ,SC.ContractNumber
  ,SC.ContractDate 
  ,SC.ContractDocumentId 
  ,SC.Description 
  from Logistics.LogisticsRequest LR
  Inner Join Logistics.SupplierContract SC on LR.Id = SC.LogisticsRequestId
  where LR.Id = @Id and LR.CreatedBy = @userId



  ------- paymentCommandInfo
  select
  pc.Id
  ,pc.CommandDate
  ,pc.CreatedAt
  ,pc.CommandDescription
  ,(P.FirstName + ' ' + P.LastName) CreatedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentCommand Pc on pc.LogisticsRequestId = LR.Id
  INNER JOIN Authentication.Users U on Pc.CreatedBy = U.Id
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
  where LR.Id = @Id and pc.IsPrePayment <> '1' and LR.CreatedBy = @userId


  -------- paymentHistoryInfo
  select
  PH.PaymentCommandId
  ,ph.PaymentDate
  ,ph.PaymentNumber
  ,ph.PaymentTypeId
  ,ph.PaymentValue
  ,pt.Name PaymentTypeName
  ,ph.Description PaymentDescription
  ,ph.PaymentDocumentId
  ,(P.FirstName + ' ' + P.LastName) CreatedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentHistory PH on Ph.LogisticsRequestId = LR.Id
  inner join Bank.PaymentType pt on pt.Id = PH.PaymentTypeId
  INNER JOIN Authentication.Users U on PH.CreatedBy = U.Id
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
  where LR.Id = @Id and ph.IsPrePayment <> '1' and LR.CreatedBy = @userId


------- prepaymentCommandInfo
select
  pc.Id
  ,pc.CommandDate
  ,pc.CreatedAt
  ,pc.CommandDescription
 ,(P.FirstName + ' ' + P.LastName) CreatedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentCommand Pc on pc.LogisticsRequestId = LR.Id
  INNER JOIN Authentication.Users U on Pc.CreatedBy = U.Id
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
  where LR.Id = @Id and pc.IsPrePayment = '1' and LR.CreatedBy = @userId

  ---------prepaymentHistoryInfo
  select
  PH.PaymentCommandId
  ,ph.PaymentDate
  ,ph.PaymentNumber
  ,ph.PaymentValue
  ,ph.PaymentTypeId
  ,pt.Name PaymentTypeName
  ,ph.Description PaymentDescription
  ,ph.PaymentDocumentId
 ,(P.FirstName + ' ' + P.LastName) CreatedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentHistory PH on Ph.LogisticsRequestId = LR.Id
  inner join Bank.PaymentType pt on pt.Id = PH.PaymentTypeId
  INNER JOIN Authentication.Users U on PH.CreatedBy = U.Id
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
  where LR.Id = @Id and ph.IsPrePayment = '1' and LR.CreatedBy = @userId


  -------- receiveInfo
  select 
  RO.ReceiveDate
  ,RO.ReceiptNumber
  ,RO.ReceiptDocumentId
  ,RO.Description
  ,(p.FirstName + ' ' + p.LastName) ReceivedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.ReceiveOrder RO on LR.Id = RO.LogisticsRequestId
  inner join Authentication.Users u on u.Id = RO.CreatedBy
  inner join Authentication.Profile p on p.Id = u.ProfileID
  where LR.Id = @Id and LR.CreatedBy = @userId




  -------- deliveryInfo
  select
  DO.DeliveryDate
  ,Do.ReceiptDocumentId
  ,do.Description
  ,(p.FirstName + ' ' + p.LastName) DeliveredBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.DeliveryOrder DO on LR.Id = DO.LogisticsRequestId
  inner join Authentication.Users u on u.Id = DO.CreatedBy
  inner join Authentication.Profile p on p.Id = u.ProfileID
  where LR.Id = @Id and LR.CreatedBy = @userId


  ------- returnInfo
  select
  DO.ReturnDate
  ,do.Description
  ,(p.FirstName + ' ' + p.LastName) ReturnedBy
  from Logistics.LogisticsRequest LR
  inner join Logistics.ReturnOrder DO on LR.Id = DO.LogisticsRequestId
  inner join Authentication.Users u on u.Id = DO.CreatedBy
  inner join Authentication.Profile p on p.Id = u.ProfileID
  where LR.Id = @Id and LR.CreatedBy = @userId




             ------ ReceiptDocument
             select 
                  LRD2.Id
                  ,d.FileAddress DocumentPath
                  ,DT.Name DocumentTypeName
                  ,d.DocumentTypeId
                  ,d.FileExtensionId
                  ,DE.Name DocumentExtensionName
,S.Name AttachStepName
             FROM Logistics.LogisticsRequest LR
             inner join Logistics.ReceiveOrder RO on RO.LogisticsRequestId = LR.Id
             inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RO.ReceiptDocumentId
             inner join DMS.Documents D on LRD2.DocumentId = d.Id
             inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
             inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
left join Project.Step S on S.Id = D.AttachStepId
             WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId


---- goodsCoding
			select
gc.GoodsId
,g.Name GoodsName
,gc.Code
,GCA.Id GoodsCategoryId
,GCA.Name GoodsCategoryName
,GT.Id GoodsTypeId
,GT.Name GoodsTypeName
,S.Name AttachStepName
FROM Logistics.LogisticsRequest LR
INNER JOIN Logistics.LogisticsRequestGoods LRG ON LRG.LogisticsRequestId = LR.Id
INNER JOIN Logistics.Goods G on G.Id = LRG.GoodsId
inner join Logistics.GoodsCoding gc on gc.GoodsId = G.Id
left join Logistics.GoodsCategory GCA on GCA.Id = G.GoodsCategoryId
left join Logistics.GoodsType GT on GCA.GoodsTypeId= GT.Id
left join Project.Step S on S.Id = D.AttachStepId
WHERE LR.Id = @Id and LR.CreatedBy = @userId

 -------- documents
 select 
   D.Id
   ,d.FileAddress DocumentPath
   ,d.Name DocumentFileName
   ,DT.Name DocumentTypeName
   ,d.DocumentTypeId
   ,d.FileExtensionId
   ,DE.Name DocumentExtensionName
   ,(p.FirstName + ' ' + p.LastName) CreatedBy
   ,LRD2.CreatedAt
 FROM Logistics.LogisticsRequest LR
 inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.LogisticsRequestId = LR.Id
 inner join DMS.Documents D on LRD2.DocumentId = d.Id
 inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
 inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
 left join Authentication.Users u on u.Id = LRD2.CreatedBy
 left join Authentication.Profile p on p.Id = u.ProfileID
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3 and LR.CreatedBy = @userId
";


            using (var multi = await connection.QueryMultipleAsync(query, new { Id = logesticId, issueId = issueId, userId = _simaIdentity.UserId }))
            {
                response = (await multi.ReadAsync<GetMyLogisticCartableGetQueryResult>())?.FirstOrDefault();
                if (response is null) throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);

                response.GoodsList = multi.ReadAsync<GoodsList>().GetAwaiter().GetResult()?.ToList();
                response.IssueInfo = multi.ReadAsync<IssueInfo>().GetAwaiter().GetResult()?.FirstOrDefault();
                if (response.IssueInfo is not null)
                    response.IssueInfo.IssueCommentList = await multi.ReadAsync<GetIssueCommentQueryResult>();
                response.IssueApprovalList = (await multi.ReadAsync<IssueApprovalList>())?.ToList();
                response.PriceEstimationInfo = (await multi.ReadAsync<PriceEstimationList>())?.FirstOrDefault();
                response.CandidateSupplierList = (await multi.ReadAsync<CandidatedSupplierList>())?.ToList();
                response.InvoiceDocumentList = (await multi.ReadAsync<InvoiceDocumentList>())?.ToList();
                response.OrderingInfo = (await multi.ReadAsync<OrderingList>())?.FirstOrDefault();
                response.SupplierContractInfo = (await multi.ReadAsync<SupplierContractInfo>())?.FirstOrDefault();
                response.PaymentCommandInfo = (await multi.ReadAsync<PaymentCommandInfo>())?.FirstOrDefault();
                response.PaymentHistoryInfo = (await multi.ReadAsync<PaymentHistoryInfo>())?.FirstOrDefault();
                response.PrePaymentCommandInfo = (await multi.ReadAsync<PaymentCommandInfo>())?.FirstOrDefault();
                response.PrePaymentHistoryInfo = (await multi.ReadAsync<PaymentHistoryInfo>())?.FirstOrDefault();
                response.ReceiveInfo = (await multi.ReadAsync<ReceiveInfo>())?.FirstOrDefault();
                response.DeliveryInfo = (await multi.ReadAsync<DeliveryInfo>())?.FirstOrDefault();
                response.ReturnInfo = (await multi.ReadAsync<ReturnInfo>())?.FirstOrDefault();
                response.ReceiptDocumentList = (await multi.ReadAsync<ReceiptDocumentList>())?.ToList();
                response.GoodsCodingList = (await multi.ReadAsync<GoodsCoding>())?.ToList();
                response.DocumentList = (await multi.ReadAsync<DocumentList>())?.ToList();

            }
            response.NullCheck();
            return response;
        }
    }


    public async  Task<bool> IsTechnological(List<long> goodsId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = @"select gc.IsTechnological from Logistics.Goods g join Logistics.GoodsCategory gc on g.GoodsCategoryId = gc.Id
                                where g.Id in @goodsId";
            var temp = await connection.QueryAsync<string>(query, new { goodsId });
            var result = temp.All(t => t == "1") || temp.All(t => t == "0");
            return result;
        }
    }
    public async  Task<bool> IsGoods(List<long> goodsId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = @"select gc.IsGoods from Logistics.Goods g join Logistics.GoodsCategory gc on g.GoodsCategoryId = gc.Id
                                where g.Id in @goodsId";
            var temp = await connection.QueryAsync<string>(query, new { goodsId });
            var result = temp.All(t => t == "1") || temp.All(t => t == "0");
            return result;
        }
    }
    public async  Task<bool> IsHardware(List<long> goodsId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = @"select gc.IsHardware from Logistics.Goods g join Logistics.GoodsCategory gc on g.GoodsCategoryId = gc.Id
                                where g.Id in @goodsId";
            var temp = await connection.QueryAsync<string>(query, new { goodsId });
            var result = temp.All(t => t == "1") || temp.All(t => t == "0");
            return result;
        }
    }
}
