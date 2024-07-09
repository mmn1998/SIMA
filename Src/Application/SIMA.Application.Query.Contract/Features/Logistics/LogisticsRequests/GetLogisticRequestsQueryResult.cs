using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class GetLogisticRequestsQueryResult
{
    public long Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
    public long CreatorDepartmentId { get; set; }
    public string? CreatorDepartmentName { get; set; }
    public List<GoodsList>? GoodsList { get; set; }
    public IssueInfo? IssueInfo { get; set; }
    public List<IssueApprovalList>? IssueApprovalList { get; set; }
    public PriceEstimationList? PriceEstimationInfo { get; set; }
    public List<CandidatedSupplierList>? CandidateSupplierList { get; set; }
    public List<InvoiceDocumentList>? InvoiceDocumentList { get; set; }
    public OrderingList? OrderingInfo { get; set; }
    public SupplierContractInfo? SupplierContractInfo { get; set; }
    public PaymentCommandInfo? PaymentCommandInfo { get; set; }
    public PaymentHistoryInfo? PaymentHistoryInfo { get; set; }
    public PaymentCommandInfo? PrePaymentCommandInfo { get; set; }
    public PaymentHistoryInfo? PrePaymentHistoryInfo { get; set; }
    public ReceiveInfo? ReceiveInfo { get; set; }
    public DeliveryInfo? DeliveryInfo { get; set; }
    public ReturnInfo? ReturnInfo { get; set; }
    public List<GoodsCoding>? GoodsCodingList { get; set; }
    public List<ReceiptDocumentList>? ReceiptDocumentList { get; set; }
    public List<DocumentList>? DocumentList { get; set; }
    public List<GetRelatedProgressQueryResult>? RelatedProgresses { get; set; }
    public IEnumerable<GetApprovalOptionQueryResult>? ApprovalOptionList { get; set; }
    public IEnumerable<GetStepRequiredDocumentQueryResult>? StepRequiredDocumentList { get; set; }
}
public class SupplierContractInfo
{
    public long Id { get; set; }
    public string? ContractNumber { get; set; }
    public DateTime ContractDate { get; set; }
    public long ContractDocumentId { get; set; }
    public string? Description { get; set; }
}
public class PaymentCommandInfo
{
    public long Id { get; set; }
    public DateTime CommandDate { get; set; }
    public string? CommandDescription { get; set; }
}
public class PaymentHistoryInfo
{
    public long PaymentCommandId { get; set; }
    public string? PaymentNumber { get; set; }
    public DateTime PaymentDate { get; set; }
    public long PaymentDocumentId { get; set; }
    public string? PaymentDescription { get; set; }
    public long PaymentTypeId { get; set; }
    public string? PaymentTypeName { get; set; }
}
public class ReceiveInfo
{
    public string? ReceivedBy { get; set; }
    public string? ReceiptNumber { get; set; }
    public DateTime ReceiptDate { get; set; }
    public long ReceiptDocumentId { get; set; }
    public string? Description { get; set; }
}
public class DeliveryInfo
{
    public string? DeliveryBy { get; set; }
    public string? ReceiptNumber { get; set; }
    public DateTime DeliveryDate { get; set; }
    public long ReceiptDocumentId { get; set; }
    public string? Description { get; set; }
}
public class ReturnInfo
{
    public string? ReturnBy { get; set; }
    public DateTime ReturnDate { get; set; }
    public string? Description { get; set; }
}
public class GoodsCoding
{
    public long? GoodsId { get; set; }
    public string? Code { get; set; }
    public string? GoodsName { get; set; }
}

public class GoodsList
{
    public long Id { get; set; }
    public int Quantity { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public long UnitMeasurementId { get; set; }
    public string? unitMeasurementName { get; set; }
    public long GoodsTypeId { get; set; }
    public string? GoodsTypeName { get; set; }
    public long GoodsCategoryId { get; set; }
    public string? GoodsCategoryName { get; set; }
}

public class IssueInfo
{
    public long Id { get; set; }
    public string Code { get; set; }
    public long WorkflowId { get; set; }
    public string? HasDocument { get; set; }
    public string WorkflowName { get; set; }
    public long MainAggregateId { get; set; }
    public long IssuePriorityId { get; set; }
    public string IssuePriorityName { get; set; }
    public long IssueWeightId { get; set; }
    public string IssueWeightName { get; set; }
    public int Weight { get; set; }
    public long CurrentStateId { get; set; }
    public string CurrentStateName { get; set; }
    public long CurrenStepId { get; set; }
    public string CurrentStepName { get; set; }
    public DateTime DueDate { get; set; }
}

public class IssueApprovalList
{
    public long StepApprovalOptionId { get; set; }
    public string StepApprovalOptionName { get; set; }
    public string Description { get; set; }
    public string StepName { get; set; }
    public long StepId { get; set; }
    public long ActorId { get; set; }
    public string ActorName { get; set; }
    public string CreatedBy { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class PriceEstimationList
{
    public float EstimationPrice { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}
public class CandidatedSupplierList
{
    public long SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string IsSelected { get; set; }
    public DateTime SelectionDate { get; set; }
    public string IsWrittenInquiry { get; set; }
    public float InquieredPrice { get; set; }
}

public class InvoiceDocumentList
{
    public long Id { get; set; }
    public long InvoiceDocumentId { get; set; }
    public string InvoiceDocumentPath { get; set; }
    public string DocumentTypeName { get; set; }
    public long DocumentTypeId { get; set; }
    public long FileExtensionId { get; set; }
    public string? DocumentContentType { get; set; }
    public string DocumentExtensionName { get; set; }
}

public class OrderingList
{
    public DateTime OrderDate { get; set; }
    public string ReceiptNumber { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string IsContractRequired { get; set; }
    public string IsPrePaymentRequired { get; set; }
    public string CreatedBy { get; set; }
}

public class ReceiptDocumentList
{
    public long Id { get; set; }
    public string DocumentPath { get; set; }
    public string DocumentTypeName { get; set; }
    public long DocumentTypeId { get; set; }
    public long FileExtensionId { get; set; }
    public string DocumentExtensionName { get; set; }
    public string? DocumentContentType { get; set; }
}
public class DocumentList
{
    public long Id { get; set; }
    public string DocumentPath { get; set; }
    public string DocumentTypeName { get; set; }
    public long DocumentTypeId { get; set; }
    public long FileExtensionId { get; set; }
    public string? DocumentContentType { get; set; }
    public string DocumentExtensionName { get; set; }
}



