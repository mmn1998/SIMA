using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
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
    public async Task<Result<IEnumerable<GetLogisticRequestsQueryResult>>> GetAll(GetAllLogisticsRequestsQuery request)
    {
        throw new NotImplementedException();
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
				SELECT  
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
	               	SELECT  
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

    public async Task<Result<LogisticCartableGetQueryResult>> GetLogesticCartableDetail(long logesticId , long issueId)
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
  FROM Logistics.LogisticsRequest LR
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
     ,Step.HasDocument
 FROM Logistics.LogisticsRequest LR
 INNER JOIN IssueManagement.Issue I ON I.Id = Lr.IssueId
 INNER JOIN Project.WorkFlow W ON I.CurrentWorkflowId = W.Id
 INNER JOIN IssueManagement.IssuePriority IP ON IP.Id = I.IssuePriorityId
 INNER JOIN IssueManagement.IssueWeightCategory IWC ON IWC.Id = I.IssueWeightCategoryId
 Left Join Project.State State on State.Id = i.CurrentStateId
 left Join Project.Step Step on Step.Id = i.CurrenStepId
 WHERE LR.Id = @Id and LR.IssueId = @issueId AND LR.ActiveStatusId <> 3

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
 select
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
left join Logistics.RequestInquiry RI on RI.LogisticsRequestId = LR.Id
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
  ,pc.CommandDescription
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentCommand Pc on pc.LogisticsRequestId = LR.Id
  where LR.Id = @Id and pc.IsPrePayment <> '1'


  -------- paymentHistoryInfo
  select
  PH.PaymentCommandId
  ,ph.PaymentDate
  ,ph.PaymentNumber
  ,ph.PaymentTypeId
  ,pt.Name PaymentTypeName
  ,ph.Description PaymentDescription
  ,ph.PaymentDocumentId
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentHistory PH on Ph.LogisticsRequestId = LR.Id
  inner join Bank.PaymentType pt on pt.Id = PH.PaymentTypeId
  where LR.Id = @Id and ph.IsPrePayment <> '1'


------- prepaymentCommandInfo
select
  pc.Id
  ,pc.CommandDate
  ,pc.CommandDescription
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentCommand Pc on pc.LogisticsRequestId = LR.Id
  where LR.Id = @Id and pc.IsPrePayment = '1'

  ---------prepaymentHistoryInfo
  select
  PH.PaymentCommandId
  ,ph.PaymentDate
  ,ph.PaymentNumber
  ,ph.PaymentTypeId
  ,pt.Name PaymentTypeName
  ,ph.Description PaymentDescription
  ,ph.PaymentDocumentId
  from Logistics.LogisticsRequest LR
  inner join Logistics.PaymentHistory PH on Ph.LogisticsRequestId = LR.Id
  inner join Bank.PaymentType pt on pt.Id = PH.PaymentTypeId
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
             FROM Logistics.LogisticsRequest LR
             inner join Logistics.ReceiveOrder RO on RO.LogisticsRequestId = LR.Id
             inner join Logistics.LogisticsRequestDocument LRD2 on LRD2.Id = RO.ReceiptDocumentId
             inner join DMS.Documents D on LRD2.DocumentId = d.Id
             inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
             inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
             WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3


---- goodsCoding
			 select
			 gc.GoodsId
			 ,g.Name GoodsName
			 ,gc.Code
			 FROM Logistics.LogisticsRequest LR
			 INNER JOIN Logistics.LogisticsRequestGoods LRG ON LRG.LogisticsRequestId = LR.Id
		     INNER JOIN Logistics.Goods G on G.Id = LRG.GoodsId
			 inner join Logistics.GoodsCoding gc on gc.GoodsId = G.Id
			 WHERE LR.Id = @Id

 -------- documents
 select 
   LRD2.Id
   ,d.FileAddress DocumentPath
   ,d.Name DocumentFileName
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


-------- Progress
    Select distinct
            FStep.Id ProgressId
            ,s.id 
            ,s.Name
            ,P.HasStoreProcedure HasStoredProcedure
            ,FStep.Name TargetName
            ,case when p.HasStoreProcedure = '1' then Convert(varchar, PSPP.Id) + ':' + PSPP.Name + ':' + IsNull(PSPP.IsSystemParam, ' ')  + ':' + IsNull(PSPP.SystemParamName, ' ') + ':' +IsNull(PSPP.DisplayName, ' ') else null end as ProcedureInfo
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
            left join [Project].ProgressStoreProcedure PSP on PSP.ProgressId = P.Id
            left join Project.ProgressStoreProcedureParam PSPP on PSP.Id = pspp.ProgressStoreProcedureId
            inner   join  [Project].[Step] ST on P.TargetId = ST.Id
            CROSS APPLY [Project].[ReturnNextStepN]   (s.id,w.Id) FStep
            Where    I.Id = @issueId and  (WU.UserID=@userId or WR.RoleId in @RoleIds or WG.GroupId in @GroupIds)
      and I.ActiveStatusId <> 3 and P.ActiveStatusId <> 3 and S.ActiveStatusId <> 3 and ST.ActiveStatusId <> 3

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
                    response = multi.ReadAsync<LogisticCartableGetQueryResult>().GetAwaiter().GetResult().FirstOrDefault();
                    response.GoodsList = multi.ReadAsync<GoodsList>().GetAwaiter().GetResult().ToList();
                    response.IssueInfo = multi.ReadAsync<IssueInfo>().GetAwaiter().GetResult().FirstOrDefault();
                    response.IssueApprovalList = multi.ReadAsync<IssueApprovalList>().GetAwaiter().GetResult().ToList();
                    response.PriceEstimationInfo = multi.ReadAsync<PriceEstimationList>().GetAwaiter().GetResult().FirstOrDefault();
                    response.CandidateSupplierList = multi.ReadAsync<CandidatedSupplierList>().GetAwaiter().GetResult().ToList();
                    response.InvoiceDocumentList = multi.ReadAsync<InvoiceDocumentList>().GetAwaiter().GetResult().ToList();
                    response.OrderingInfo = multi.ReadAsync<OrderingList>().GetAwaiter().GetResult().FirstOrDefault();
                    response.SupplierContractInfo = multi.ReadAsync<SupplierContractInfo>().GetAwaiter().GetResult().FirstOrDefault(); 
                    response.PaymentCommandInfo = multi.ReadAsync<PaymentCommandInfo>().GetAwaiter().GetResult().FirstOrDefault(); 
                    response.PaymentHistoryInfo = multi.ReadAsync<PaymentHistoryInfo>().GetAwaiter().GetResult().FirstOrDefault(); 
                    response.PrePaymentCommandInfo = multi.ReadAsync<PaymentCommandInfo>().GetAwaiter().GetResult().FirstOrDefault(); 
                    response.PrePaymentHistoryInfo = multi.ReadAsync<PaymentHistoryInfo>().GetAwaiter().GetResult().FirstOrDefault(); 
                    response.ReceiveInfo = multi.ReadAsync<ReceiveInfo>().GetAwaiter().GetResult().FirstOrDefault(); 
                    response.DeliveryInfo = multi.ReadAsync<DeliveryInfo>().GetAwaiter().GetResult().FirstOrDefault(); 
                    response.ReturnInfo = multi.ReadAsync<ReturnInfo>().GetAwaiter().GetResult().FirstOrDefault(); 
                    response.ReceiptDocumentList = multi.ReadAsync<ReceiptDocumentList>().GetAwaiter().GetResult().ToList();
                    response.GoodsCodingList = multi.ReadAsync<GoodsCoding>().GetAwaiter().GetResult().ToList();
                    response.DocumentList = multi.ReadAsync<DocumentList>().GetAwaiter().GetResult().ToList();
                    response.RelatedProgresses = multi.ReadAsync<GetRelatedProgressQueryResult>().GetAwaiter().GetResult().ToList();
                    response.StepRequiredDocumentList = multi.ReadAsync<GetStepRequiredDocumentQueryResult>().GetAwaiter().GetResult().ToList();
                    response.ApprovalOptionList = multi.ReadAsync<GetApprovalOptionQueryResult>().GetAwaiter().GetResult().ToList();
                }
                var paramList = new List<StoreProcedureParams>();
                var groupedResults = response.RelatedProgresses.GroupBy(x => new { x.ProgressId/*, x.ProgressStoreProcedureId */})
                    .Select(g => new
                    {
                        ProgressId = g.Key.ProgressId,
                        // ProgressStoreProcedureId = g.Key.ProgressStoreProcedureId
                        Items = g.ToList()
                    });
                foreach (var progress in response.RelatedProgresses)
                {
                    if (progress.HasStoredProcedure == "1" && !string.IsNullOrEmpty(progress.ProcedureInfo) && progress.ProcedureInfo.Contains(":"))
                    {
                        var obj = new StoreProcedureParams();
                        /// <summary>
                        /// splitedinfo[0] is ProgressStoreProcedureId, splitedinfo[1] is Name, splitedinfo[2] is IsSystemParam, splitedinfo[3] is SystemParam and splitedinfo[4] is DisplayName
                        /// </summary>
                        var splitedinfo = progress.ProcedureInfo.Split(":");
                        obj.ProgressStoredProcedureParamId = Convert.ToInt64(splitedinfo[0].ToString());
                        obj.Name = splitedinfo[1];
                        obj.DisplayName = splitedinfo[4];
                        if (splitedinfo[2] == "0")
                        {
                            paramList.Add(obj);
                        }
                    }
                }
                response.RelatedProgresses.ToList().Clear();
                response.RelatedProgresses = new();
                foreach (var item in groupedResults)
                {
                    var obj = new GetRelatedProgressQueryResult
                    {
                        Params = paramList,
                        Id = item.Items[0].Id,
                        Name = item.Items[0].Name,
                        ProgressId = item.ProgressId,
                        TargetId = item.Items[0].TargetId,
                        TargetName = item.Items[0].TargetName,
                        //ProgressStoreProcedureId = item.ProgressStoreProcedureId,
                    };
                    response.RelatedProgresses.Add(obj);
                }
                response.NullCheck();
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
