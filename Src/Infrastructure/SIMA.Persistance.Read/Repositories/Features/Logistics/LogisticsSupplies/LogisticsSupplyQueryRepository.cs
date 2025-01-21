using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsSupplies;

public class LogisticsSupplyQueryRepository : ILogisticsSupplyQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    private readonly ISimaIdentity _simaIdentity;

    public LogisticsSupplyQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity)
    {
        _connectionString = configuration.GetConnectionString();
        _simaIdentity = simaIdentity;
        _mainQuery = @"
select distinct
LS.Id,
Ls.Description,
LS.CreatedAt,
(P.FirstName + ' ' + p.LastName) CreatedBy,
D.Id CreatorDepartmentId,
D.Name CreatorDepartmentName,
A.ID ActiveStatusId,
A.Name ActiveStatusName,
I.Id IssueId,
I.Code IssueCode,
I.CurrentWorkflowId WorkflowId,
W.Name WorkflowName,
I.MainAggregateId,
I.IssuePriorityId,
IPP.Name IssuePriorityName,
I.IssueWeightCategoryId IssueWeightId,
IWC.Name IssueWeightName,
I.Weight,
S.HasDocument,
I.CurrenStepId,
I.CurrentStateId,
st.Name CurrentStateName,
S.Name CurrentStepName,
I.DueDate
from Logistics.LogisticsSupply LS
join IssueManagement.Issue I  on I.Id= LS.IssueId and I.SourceId=LS.Id AND I.MainAggregateId = 7
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
left join IssueManagement.IssueWeightCategory IWC on IWC.Id = I.IssueWeightCategoryId
join  [Authentication].[Users] U on I.CreatedBy = U.Id
join  [Authentication].[Profile] P on P.Id = U.ProfileID
left join Organization.Staff Staff on Staff.ProfileId = P.Id
left join Organization.Position Po On Po.Id = Staff.PositionId
left join Organization.Department D on D.Id = Po.DepartmentId
join Basic.ActiveStatus a on LS.ActiveStatusId=a.ID 
left join  IssueManagement.IssueManager IM  on IM.IssueId=I.id
";
    }
    public async Task<Result<IEnumerable<GetLogisticsSupplyQueryResult>>> GetAll(GetAllLogisticsSuppliesQuery request)
    {
        var userId = _simaIdentity.UserId;
        var roleIds = _simaIdentity.RoleIds;
        var groupIds = _simaIdentity.GroupId;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = $@"  WITH Query as(
				{_mainQuery}

                    WHERE ( (IsDirectManagerOfIssueCreator=0 and  (WU.UserID=@userId or WR.RoleID IN @RoleIds or WG.GroupID IN @GroupIds) )
                                  or ( IsDirectManagerOfIssueCreator=1 and IM.UserId=@userId  ))
                                  and
                     I.ActiveStatusId != 3 and LS.ActiveStatusId != 3 and W.ActiveStatusId != 3
                     and ((ISNULL(wa.IsActorManager, '0') = '1' OR ISNULL(S.IsAssigneeForced, '0') = 0 OR (I.AssigneeId Is Not Null And I.AssigneeId = WA.Id))

                    ))
                    SELECT Count(*) FROM Query
                     /**where**/
                     ; ";
            string query = $@" WITH Query as({_mainQuery}
                    WHERE ( (IsNull(IsDirectManagerOfIssueCreator,0)=0  and  (WU.UserID=@userId or WR.RoleID IN @RoleIds or WG.GroupID IN @GroupIds) )
                                  or ( ISNULL(IsDirectManagerOfIssueCreator,0)=1  and IM.UserId=@userId  ))
                                  and
                     I.ActiveStatusId != 3 and LS.ActiveStatusId != 3 and W.ActiveStatusId != 3 
                      and ((ISNULL(wa.IsActorManager, '0') = '1' OR ISNULL(S.IsAssigneeForced, '0') = 0 OR (I.AssigneeId Is Not Null And I.AssigneeId = WA.Id))
							))
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
                var response = await multi.ReadAsync<GetLogisticsSupplyQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
    public async Task<Result<IEnumerable<GetLogisticsSupplyQueryResult>>> GetAllMy(GetAllMyLogisticsSuppliesQuery request)
    {
        var userId = _simaIdentity.UserId;
        var roleIds = _simaIdentity.RoleIds;
        var groupIds = _simaIdentity.GroupId;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = $@"  WITH Query as(
				{_mainQuery}
                Where LS.CreatedBy = @userId or I.RequesterId = @userId
)
                    SELECT Count(*) FROM Query
                     /**where**/
                     ; ";
            string query = $@" WITH Query as({_mainQuery}
                                              Where (LS.CreatedBy = @userId or I.RequesterId = @userId) and LS.ActiveStatusId <>3                           
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
                var response = await multi.ReadAsync<GetLogisticsSupplyQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
    public async Task<GetLogisticsSupplyDeatilQueryResult> GetDetail(long id, long logisticsRequestId)
    {
        try
        {
            var response = new GetLogisticsSupplyDeatilQueryResult();
            var query = @"
	SELECT
                           LR.Id
                          ,LR.Description
                          ,LR.CreatedAt
                          ,(p.FirstName + ' ' + P.LastName) CreatedBy
                          ,LR.ActiveStatusId
		                  ,LR.PayByFundCard
                          ,a.Name ActiveStatus
                          ,D.Name CreatorDepartmentName
                          ,d.Id CreatorDepartmentId
                FROM Logistics.LogisticsSupply LR
                INNER JOIN Authentication.Users U on LR.CreatedBy = U.Id
                INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
                INNER JOIN Basic.ActiveStatus A on A.ID = LR.ActiveStatusId
                left join Organization.Staff S on S.ProfileId = P.Id
                left join Organization.Position PO on po.Id = s.PositionId
                left join Organization.Department D on d.Id = Po.DepartmentId
                  WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3

  --LogisticsSupplyGoods

              select 
                          LSG.Id
                         ,UM.Id UnitMeasurementId
                         ,UM.Name UnitMeasurementName
                         ,GT.Id GoodsTypeId
                         ,GT.Name GoodsTypeName
                         ,GC.Id GoodsCategoryId
                         ,GC.Name GoodsCategoryName
                         ,LSG.Description
                         ,LRG.Quantity
		                 ,LRG.ServiceDuration
                         ,LRG.UsageDuration
                         ,LRG.LogisticsRequestId
                         ,LSG.IsContractRequired
                         ,LSG.IsPrePaymentRequired
                         ,LSG.PrePaymentPercentage
                         ,LR.Code logisticsRequestCode
                         ,LSG.CreatedAt RequestDateTime
                         ,I.RequesterId
                         ,(PP.FirstName + ' ' + PP.LastName) RequesterName
                         ,LSG.EstimatedPrice
                         ,I.CreatedAt RequestDateTime
                         ,GS.Id GoddsStatusId
                         ,GS.Name GoodsStatusName
                 FROM Logistics.LogisticsSupply AS LS 
                 INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                 INNER JOIN Logistics.LogisticsRequestGoods AS LRG ON lsg.LogisticsRequestGoodsId = LRG.Id 
                 INNER JOIN Logistics.LogisticsRequest AS LR ON LRG.LogisticsRequestId = LR.Id
                 join IssueManagement.Issue I on I.SourceId = LR.Id and I.ActiveStatusId <> 3
                 join Authentication.Users U on I.CreatedBy = U.Id and U.ActiveStatusId<>3
                 join Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
                 left join Authentication.Users Uu on I.RequesterId = UU.Id and UU.ActiveStatusId<>3
                 left join Authentication.Profile Pp on PP.Id = UU.ProfileID and PP.ActiveStatusId<>3
                 join Logistics.GoodsCategory GC on GC.Id = LRG.GoodsCategoryId and GC.ActiveStatusId <> 3
                 join Logistics.GoodsType GT on GT.Id = GC.GoodsTypeId and GT.ActiveStatusId <> 3
                 left join Logistics.Goods G on LRG.GoodsId = G.Id  and G.ActiveStatusId <> 3
                 left join Logistics.UnitMeasurement UM on g.UnitMeasurementId = UM.Id and UM.ActiveStatusId <> 3
                 left join Logistics.GoodsStatus GS on GS.Id = LRG.GoodsStatusId and GS.ActiveStatusId<>3
                 WHERE LS.Id = @Id  and LS.ActiveStatusId <> 3



 ---- CandidatedSupplier
                select distinct
                        CS.SupplierId
                       ,S.Name SupplierName
                       ,CS.IsSelected
                       ,CS.SelectionDate
                       ,RI.IsWrittenInquiry
                       ,RI.InquieredPrice
                       ,D.Id InvoiceDocumentId
                       ,D.Name InvoiceDocumentName
	                   ,DE.Name DocumentExtensionName
                       ,CS.CreatedAt 
                       ,(p4.FirstName + ' ' + P4.LastName) CreatedBy
                FROM Logistics.LogisticsSupply AS LS 
                INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                INNER JOIN Logistics.LogisticsRequestGoods AS LRG ON lsg.LogisticsRequestGoodsId = LRG.Id 
                INNER JOIN Logistics.LogisticsRequest AS LR ON LRG.LogisticsRequestId = LR.Id
                INNER JOIN Logistics.CandidatedSupplier CS on CS.LogisticsSupplyId = LSG.LogisticsSupplyId and CS.ActiveStatusId <> 3
                inner join Basic.Supplier S on CS.SupplierId = s.Id and S.ActiveStatusId <> 3 
                inner join Logistics.RequestInquiry RI on RI.CandidatedSupplierId = CS.Id and RI.ActiveStatusId <> 3
                left join Logistics.LogisticsSupplyDocument LSD on LSD.Id = RI.InvoiceDocumentId and RI.ActiveStatusId<>3
                left join DMS.Documents D on LSD.DocumentId = D.Id and d.ActiveStatusId<>3
                left join DMS.DocumentExtension DE on DE.Id = d.FileExtensionId and d.ActiveStatusId<>3
                INNER JOIN Authentication.Users U4 on LS.CreatedBy = U4.Id and U4.ActiveStatusId <> 3
                INNER JOIN Authentication.Profile P4 on P4.Id = U4.ProfileID and P4.ActiveStatusId <> 3
              WHERE LS.Id = @Id AND LS.ActiveStatusId <> 3



 ------ Ordering
                      select distinct
                       o.Id orderingId
                       ,S.Id SupplierId
                       ,S.Name SupplierName
                       ,o.OrderDate
                       ,o.ReceiptNumber
                       ,d.Id ReceiptDocumentId
                       ,D.Name ReceiptDocumentName
                       ,DE.Name DocumentExtensionName
                       ,o.Description
                       ,o.CreatedAt
                       ,(p.FirstName + ' ' + P.LastName) CreatedBy
                    FROM Logistics.LogisticsSupply AS LS 
                    INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                    INNER JOIN Logistics.LogisticsRequestGoods AS LRG ON lsg.LogisticsRequestGoodsId = LRG.Id 
                    inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                    left join Logistics.LogisticsSupplyDocument LSD on LSD.Id = o.ReceiptDocumentId and LSD.ActiveStatusId<>3
                    left join DMS.Documents D on LSD.DocumentId = D.Id and d.ActiveStatusId<>3
                    left join DMS.DocumentExtension DE on DE.Id = d.FileExtensionId and d.ActiveStatusId<>3
                    INNER JOIN Logistics.CandidatedSupplier CS on CS.LogisticsSupplyId = LSG.LogisticsSupplyId and CS.ActiveStatusId <> 3
                    inner join Basic.Supplier S on CS.SupplierId = s.Id and S.ActiveStatusId <> 3 
                    INNER JOIN Authentication.Users U on LS.CreatedBy = U.Id
                    INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
                     WHERE LS.Id = @Id AND LS.ActiveStatusId <> 3


 ---orderingItem 

                   select distinct
                           LSG.Id logisticsSupplyGoodsId
	                      ,GC.Name GoodsCategoryName
	                      ,GC.Id GoodsCategoryId
	                      ,G.Id GoodsId
	                      ,G.Name GoodsName
                          ,OI.ItemPrice
                          ,O.Id OrderingId
                     FROM Logistics.LogisticsSupply AS LS 
                     INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                     Join Logistics.LogisticsRequestGoods LRG on LRG.Id = LSG.LogisticsRequestGoodsId and LRG.ActiveStatusId <> 3
                     join Logistics.GoodsCategory GC on GC.Id = LRG.GoodsCategoryId  and GC.ActiveStatusId <> 3
                     left join Logistics.Goods G on G.Id = LRG.GoodsId and G.ActiveStatusId <> 3
                     inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                     join Logistics.OrderingItem OI on OI.OrderingId = O.Id and OI.ActiveStatusId <> 3
                     INNER JOIN Authentication.Users U on O.CreatedBy = U.Id
                     INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
                     WHERE LS.Id = @Id AND LS.ActiveStatusId <> 3

  -------- supplierContract
                    select distinct
      SC.Id
     ,S.Id SupplierId
     ,S.Name SupplierName
     ,SC.ContractNumber
     ,SC.ContractDate 
	 ,SC.CreatedAt
     ,SC.ContractDocumentId 
     ,SC.Description 
	 ,(P.FirstName + ' ' + P.LastName) CreatedBy
 FROM Logistics.LogisticsSupply AS LS 
 INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
join Logistics.CandidatedSupplier CS on LSG.LogisticsSupplyId = CS.LogisticsSupplyId and CS.ActiveStatusId <> 3 and CS.IsSelected =1
join Logistics.SupplierContract SC on SC.CandidatedSupplierId = CS.Id  and SC.ActiveStatusId <> 3
join Basic.Supplier S on CS.SupplierId = S.Id and S.ActiveStatusId <> 3
JOIN Authentication.Users U on SC.CreatedBy = U.Id
JOIN Authentication.Profile P on P.Id = U.ProfileID
                    where LS.Id = @Id and LS.ActiveStatusId <> 3

  ------- paymentCommandInfo
                    select distinct
	                   PC.Id
	                  ,O.Id OrderingId
	                  ,pc.CommandDate
	                  ,pc.CreatedAt
	                  ,pc.CommandDescription
	                  ,(P.FirstName + ' ' + P.LastName) CreatedBy
                 FROM Logistics.LogisticsSupply AS LS 
                INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                inner join Logistics.PaymentCommand Pc on pc.OrderingId = O.Id and Pc.ActiveStatusId <> 3
                INNER JOIN Authentication.Users U on PC.CreatedBy = U.Id
                INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
                 where LS.Id = @Id and pc.IsPrePayment <> '1' and LS.ActiveStatusId <> 3

  -------- paymentHistoryInfo
                  select distinct
                           PH.Id
                          ,PH.OrderingId
                          ,ph.PaymentCommandId
                          ,PH.PaymentDate
                          ,ph.PaymentNumber
                          ,ph.PaymentTypeId
                          ,ph.PaymentValue
                          ,pt.Name PaymentTypeName
                          ,ph.Description PaymentDescription
                          ,d.Id PaymentDocumentId
                          ,D.Name PaymentDocumentName
                          ,de.Name DocumentExtensionName
                          ,(P.FirstName + ' ' + P.LastName) CreatedBy
                 FROM Logistics.LogisticsSupply AS LS 
                INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                 inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                 inner join Logistics.PaymentCommand Pc on pc.OrderingId = O.Id and Pc.ActiveStatusId <> 3
                 join Logistics.PaymentHistory PH on PH.PaymentCommandId = PC.Id and PH.ActiveStatusId <> 3
                 Join Bank.PaymentType PT on PT.Id = PH.PaymentTypeId and PT.ActiveStatusId <> 3
                 left join Logistics.LogisticsSupplyDocument LSD on LSD.Id = ph.PaymentDocumentId and LSD.ActiveStatusId<>3
                left join DMS.Documents D on LSD.DocumentId = D.Id and d.ActiveStatusId<>3
                left join DMS.DocumentExtension DE on DE.Id = d.FileExtensionId and d.ActiveStatusId<>3
                 INNER JOIN Authentication.Users U on PH.CreatedBy = U.Id
                 INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
                  where LS.Id = @Id and ph.IsPrePayment <> '1'
  
------- prepaymentCommandInfo
                 select distinct
	                   PC.Id
	                  ,pc.CommandDate
	                  ,pc.CommandDescription
	                  ,pc.CreatedAt
	                  ,(P.FirstName + ' ' + P.LastName) CreatedBy
                 FROM Logistics.LogisticsSupply AS LS 
                INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                inner join Logistics.PaymentCommand Pc on pc.OrderingId = O.Id and Pc.ActiveStatusId <> 3
                INNER JOIN Authentication.Users U on Pc.CreatedBy = U.Id
                INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
                 where LS.Id = @Id and pc.IsPrePayment = '1' and LS.ActiveStatusId <> 3

  ---------prepaymentHistoryInfo
                         select distinct
                          ph.PaymentCommandId
                          ,PH.PaymentDate
                          ,ph.PaymentNumber
                          ,ph.PaymentTypeId
                          ,ph.PaymentValue
                          ,pt.Name PaymentTypeName
                          ,ph.Description PaymentDescription
                          ,d.Id PaymentDocumentId
                          ,D.Name PaymentDocumentName
                          ,DE.Name DocumentExtensionName
                          ,(P.FirstName + ' ' + P.LastName) CreatedBy
                        FROM Logistics.LogisticsSupply AS LS 
                        INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                        inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                        inner join Logistics.PaymentCommand Pc on pc.OrderingId = O.Id and Pc.ActiveStatusId <> 3
                        join Logistics.PaymentHistory PH on PH.PaymentCommandId = PC.Id and PH.ActiveStatusId <> 3
                        left join Logistics.LogisticsSupplyDocument LSD on LSD.Id = ph.PaymentDocumentId and LSD.ActiveStatusId<>3
                        left join DMS.Documents D on LSD.DocumentId = D.Id and d.ActiveStatusId<>3
                        left join DMS.DocumentExtension DE on DE.Id = d.FileExtensionId and d.ActiveStatusId<>3
                        Join Bank.PaymentType PT on PT.Id = PH.PaymentTypeId and PT.ActiveStatusId <> 3
                        INNER JOIN Authentication.Users U on PH.CreatedBy = U.Id
                        INNER JOIN Authentication.Profile P on P.Id = U.ProfileID
                         where LS.Id = @Id and ph.IsPrePayment = '1'

  -------- receiveInfo
                           select distinct
                               O.Id OrderingId
                              ,RO.ReceiveDate
                              ,RO.ReceiptNumber
                              ,d.Id ReceiptDocumentId
                                ,D.Name ReceiptDocumentName
                                ,De.Name DocumentExtensionName
                              ,RO.Description
                              ,(p.FirstName + ' ' + p.LastName) ReceivedBy
                        FROM Logistics.LogisticsSupply AS LS 
                        INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                        inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                        join Logistics.ReceiveOrder RO on RO.OrderingId = O.Id and RO.ActiveStatusId<> 3
                        left join Logistics.LogisticsSupplyDocument LSD on LSD.Id = RO.ReceiptDocumentId and LSD.ActiveStatusId<>3
                        left join DMS.Documents D on LSD.DocumentId = D.Id and d.ActiveStatusId<>3
                        left join DMS.DocumentExtension DE on DE.Id = d.FileExtensionId and d.ActiveStatusId<>3
                        inner join Authentication.Users u on u.Id = RO.CreatedBy
                        inner join Authentication.Profile p on p.Id = u.ProfileID
                         where LS.Id = @Id and LS.ActiveStatusId<>3
  
  -------- deliveryInfo
                    select distinct
		                   DI.OrderingItemId
		                  ,DI.DeliveryQuantity
		                  ,DI.DeliveryDate
		                  ,O.ReceiptNumber
		                  ,D.Id ReciptDocumentId
                            ,D.Name ReciptDocumentName
                            ,De.Name DocumentExtensionName
		                  ,DI.Description
		                  ,(p.FirstName + ' ' + p.LastName) DeliveredBy
                 FROM Logistics.LogisticsSupply AS LS 
                INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                  inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                  join Logistics.ReceiveOrder RO on RO.OrderingId = O.Id and RO.ActiveStatusId<> 3
                  join Logistics.OrderingItem OI on OI.OrderingId = O.Id and OI.ActiveStatusId<> 3
                  join Logistics.DeliveryItem DI on DI.OrderingItemId = OI.Id and DI.ActiveStatusId<> 3
                    left join Logistics.LogisticsSupplyDocument LSD on LSD.Id = DI.ReciptDocumentId and LSD.ActiveStatusId<>3
                    left join DMS.Documents D on LSD.DocumentId = D.Id and d.ActiveStatusId<>3
                    left join DMS.DocumentExtension DE on DE.Id = d.FileExtensionId and d.ActiveStatusId<>3
                  inner join Authentication.Users u on u.Id = DI.CreatedBy
                  inner join Authentication.Profile p on p.Id = u.ProfileID
                  where LS.Id = @Id and LS.ActiveStatusId<>3

  ------- returnInfo
                 select distinct
                   ROI.OrderingItemId
                  ,ROI.ReturnQuantity
                  ,ROI.ReturnDate
                  ,O.ReceiptNumber
                  ,D.Id ReciptDocumentId
,D.Name ReciptDocumentName
,DE.Name DocumentExtensionName
                  ,ROI.Description
                  ,(p.FirstName + ' ' + p.LastName) ReturnedBy
                FROM Logistics.LogisticsSupply AS LS 
                INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                inner join Logistics.Ordering O on LSG.LogisticsSupplyId = O.LogisticsSupplyId and O.ActiveStatusId <> 3
                join Logistics.ReceiveOrder RO on RO.OrderingId = O.Id and RO.ActiveStatusId<> 3
                join Logistics.OrderingItem OI on OI.OrderingId = O.Id and OI.ActiveStatusId<> 3
                join Logistics.ReturnOrderingItem ROI on ROI.OrderingItemId = OI.Id and ROI.ActiveStatusId<> 3
                left join Logistics.LogisticsSupplyDocument LSD on LSD.Id = ROI.ReciptDocumentId and LSD.ActiveStatusId<>3
                left join DMS.Documents D on LSD.DocumentId = D.Id and d.ActiveStatusId<>3
                left join DMS.DocumentExtension DE on DE.Id = d.FileExtensionId and d.ActiveStatusId<>3
                inner join Authentication.Users u on u.Id = ROI.CreatedBy
                inner join Authentication.Profile p on p.Id = u.ProfileID
                where LS.Id = @Id and LS.ActiveStatusId<>3
  
 
---- goodsCoding
                  select distinct
			                 LSG.Id LogisticsSupplyGoodsId
			                ,g.Name GoodsName
			                ,gc.Code
			                ,GCA.Id GoodsCategoryId
			                ,GCA.Name GoodsCategoryName
			                ,GT.Id GoodsTypeId
			                ,GT.Name GoodsTypeName
                FROM Logistics.LogisticsSupply AS LS 
                INNER JOIN Logistics.LogisticsSupplyGoods AS LSG ON LS.Id = LSG.LogisticsSupplyId 
                INNER JOIN Logistics.LogisticsRequestGoods AS LRG ON lsg.LogisticsRequestGoodsId = LRG.Id 
                inner join Logistics.GoodsCoding GC on LSG.Id = GC.LogisticsSupplyGoodsId and GC.ActiveStatusId <> 3
                join Logistics.GoodsCategory GCA on GCA.Id = LRG.GoodsCategoryId and GCA.ActiveStatusId <> 3
                Join Logistics.GoodsType Gt on GT.Id = GCA.GoodsTypeId  and GT.ActiveStatusId <> 3
                left join Logistics.Goods G on G.Id = LRG.GoodsId and G.ActiveStatusId <> 3
                WHERE LS.Id = @Id and LS.ActiveStatusId<>3


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
 FROM Logistics.LogisticsSupply LR
 inner join Logistics.LogisticsSupplyDocument LRD2 on LRD2.LogisticsSupplyId = LR.Id
 inner join DMS.Documents D on LRD2.DocumentId = d.Id
 inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
 inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
 left join Authentication.Users u on u.Id = LRD2.CreatedBy
 left join Authentication.Profile p on p.Id = u.ProfileID
 left join Project.Step S on S.Id = D.AttachStepId
 WHERE LR.Id = @Id AND LR.ActiveStatusId <> 3
 order by D.CreatedAt desc
";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var multi = await connection.QueryMultipleAsync(query, new { Id = id, LogisticsRequestId = logisticsRequestId }))
                {
                    response = await multi.ReadFirstOrDefaultAsync<GetLogisticsSupplyDeatilQueryResult>() ?? throw SimaResultException.NotFound;
                    response.LogisticsSupplyGoodsList = await multi.ReadAsync<GetLogisticsSupplyGoods>();
                    response.CandidateSupplierList = await multi.ReadAsync<GetCandidateSupplier>();
                    response.OrderingList = await multi.ReadAsync<GetOrdering>();
                    var orderingItems = await multi.ReadAsync<GetOrderingItem>();
                    foreach (var ordering in response.OrderingList)
                    {
                        ordering.OrderingItemList = orderingItems.Where(i => i.OrderingId == ordering.OrderingId);
                    }
                    response.SupplierContractList = await multi.ReadAsync<GetSupplierContract>();
                    response.PaymentCommandList = await multi.ReadAsync<GetPaymentCommand>();
                    response.PaymentHistoryList = await multi.ReadAsync<GetPaymentHistory>();
                    response.PrepaymentCommandList = await multi.ReadAsync<GetPrepaymentCommand>();
                    response.PrepaymentHistoryList = await multi.ReadAsync<GetPrepaymentHistory>();
                    response.ReceiveList = await multi.ReadAsync<ReceiveInfo>();
                    response.DeliveryList = await multi.ReadAsync<DeliveryInfo>();
                    response.ReturnList = await multi.ReadAsync<ReturnInfo>();
                    response.GoodsCodingList = await multi.ReadAsync<GoodsCoding>();
                    response.DocumentList = await multi.ReadAsync<DocumentList>();

                }
            }
            return response;
        }
        catch (Exception ex)
        {

            throw;
        }
       
    }

    public async Task<Result<IEnumerable<GetLogisticsSupplyGoodsCategoryQueryResult>>> GetGoodsCategoryBySupplyId(long LogisticsSupplyId)
    {
        var response = new List<GetLogisticsSupplyGoodsCategoryQueryResult>();
        var query = @"
                  Select 
                         Ls.Id LogisticsSupplyGoodsId,
                         Ls.LogisticsRequestGoodsId,
                         LR.Code LogisticsRequestCode,
                         LSG.GoodsCategoryId,
                         GC.Name GoodsCategoryName,
		                 G.Id GoodsId,
		                 G.Name GoodsName,
		                 U.Name UnitMeasurementName,
                         LSG.Quantity,
		                 LS.EstimatedPrice,
		                 LS.IsContractRequired,
		                 Ls.IsPrePaymentRequired,
		                 Ls.PrePaymentPercentage,
		                 S.PayByFundCard
                 from Logistics.LogisticsSupplyGoods LS 
                 join Logistics.LogisticsSupply S on LS.LogisticsSupplyId = S.Id
                 join Logistics.LogisticsRequestGoods LSG on Ls.LogisticsRequestGoodsId = LSG.Id  and LSG.ActiveStatusId <> 3
                 join Logistics.LogisticsRequest LR on LR.Id = LSG.LogisticsRequestId and LR.ActiveStatusId <> 3
                 join Logistics.GoodsCategory GC on LSG.GoodsCategoryId = GC.Id and GC.ActiveStatusId <> 3
                 left Join Logistics.Goods G on G.Id = LSG.GoodsId and G.ActiveStatusId <> 3
                 left join Logistics.UnitMeasurement U on U.Id = G.UnitMeasurementId and U.ActiveStatusId <> 3
                 where LS.LogisticsSupplyId = @Id and Ls.ActiveStatusId <> 3
";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var multi = await connection.QueryMultipleAsync(query, new { Id = LogisticsSupplyId }))
            {
               var result = await multi.ReadAsync<GetLogisticsSupplyGoodsCategoryQueryResult>();

                int index = 1;

                foreach (var item in result)
                {
                    item.Index = index++;
                }

                response = result.ToList(); 
            }
        }
        return response;
    }

    public async Task<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>> GetOrderingByLogisticsSupplyId(long LogisticsSupplyId)
    {
        var query = @"
select 
O.Id,
O.ReceiptNumber Name
from Logistics.Ordering O
inner join Logistics.LogisticsSupply SP on SP.Id = O.LogisticsSupplyId
where SP.Id = @LogisticsSupplyId
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<GetOrderingByLogisticsSupplyIdQueryResult>(query, new { LogisticsSupplyId });
    }

    public async Task<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>> GetPaymentCommandByLogisticsSupplyId(long LogisticsSupplyId)
    {
        var query = @"
                select 
                PC.Id,
                PC.CommandDescription Name
                from Logistics.Ordering O
                inner join Logistics.LogisticsSupply SP on SP.Id = O.LogisticsSupplyId
                inner join Logistics.PaymentCommand PC on PC.OrderingId = O.Id
                where SP.Id = @LogisticsSupplyId and PC.IsPrePayment = '0'
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<GetOrderingByLogisticsSupplyIdQueryResult>(query, new { LogisticsSupplyId });
    }

    public async Task<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>> GetPrePaymentCommandByLogisticsSupplyId(long LogisticsSupplyId)
    {
        var query = @"
                select 
                PC.Id,
                PC.CommandDescription Name
                from Logistics.Ordering O
                inner join Logistics.LogisticsSupply SP on SP.Id = O.LogisticsSupplyId
                inner join Logistics.PaymentCommand PC on PC.OrderingId = O.Id
                where SP.Id = @LogisticsSupplyId and PC.IsPrePayment = '1'
                    ";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<GetOrderingByLogisticsSupplyIdQueryResult>(query, new { LogisticsSupplyId });
    }
}
