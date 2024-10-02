using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class GetLogisticRequestsQueryResult
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
    public string? CreatedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
    public long CreatorDepartmentId { get; set; }
    public string? IsAssigneeForced { get; set; }
    public string? IsActorManager { get; set; }
    public string? CreatorDepartmentName { get; set; }
    public List<GoodsList>? GoodsList { get; set; }
    //public IssueInfo? IssueInfo { get; set; }
    //public List<IssueApprovalList>? IssueApprovalList { get; set; }
    public PriceEstimationList? PriceEstimationInfo { get; set; }
    public IEnumerable<CandidatedSupplierList>? CandidateSupplierList { get; set; }
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
    //public IEnumerable<GetRelatedProgressQueryResult>? RelatedProgressList { get; set; }
    //public IEnumerable<GetApprovalOptionQueryResult>? ApprovalOptionList { get; set; }
    //public IEnumerable<GetStepRequiredDocumentQueryResult>? StepRequiredDocumentList { get; set; }
    //public string? UIPropertyBoxTitle { get; set; }
   // public IEnumerable<StoreProcedureParams>? FormParams { get; set; }
    //public string? IsEditable { get; set; }

}



