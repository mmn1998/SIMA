using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;


public class GetLogisticsSupplyQueryResult
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public long? CreatorDepartmentId { get; set; }
    public string? CreatorDepartmentName { get; set; }
    public long? ActiveStatusId { get; set; }
    public string? ActiveStatusName { get; set; }
    public long? IssueId { get; set; }
    public string? IssueCode { get; set; }
    public long WorkflowId { get; set; }
    public string? WorkflowName { get; set; }
    public long MainAggregateId { get; set; }
    public long? IssuePriorityId { get; set; }
    public string? IssuePriorityName { get; set; }
    public long? IssueWeightId { get; set; }
    public string? IssueWeightName { get; set; }
    public int? Weight { get; set; }
    public long? CurrentStateId { get; set; }
    public string? CurrentStateName { get; set; }
    public string? HasDocument { get; set; }
    public long? CurrentStepId { get; set; }
    public string? CurrentStepName { get; set; }
    public DateTime? DueDate { get; set; }
    public string? DueDatePersian => DueDate.ToPersianDateTime();
}

public class GetLogisticsSupplyDeatilQueryResult
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public string? PayByFundCard { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public long? CreatorDepartmentId { get; set; }
    public string? CreatorDepartmentName { get; set; }
    public long? ActiveStatusId { get; set; }
    public string? ActiveStatusName { get; set; }
    public IEnumerable<GetLogisticsSupplyGoods>? LogisticsSupplyGoodsList { get; set; }
    public GetPriceEstimationInfo? PriceEstimationInfo { get; set; }
    public IEnumerable<GetCandidateSupplier>? CandidateSupplierList { get; set; }
    public IEnumerable<GetOrdering>? OrderingList { get; set; }
    public IEnumerable<GetSupplierContract>? SupplierContractList { get; set; }
    public IEnumerable<GetPaymentCommand>? PaymentCommandList { get; set; }
    public IEnumerable<GetPaymentHistory>? PaymentHistoryList { get; set; }
    public IEnumerable<GetPrepaymentCommand>? PrepaymentCommandList { get; set; }
    public IEnumerable<GetPrepaymentHistory>? PrepaymentHistoryList { get; set; }
    public IEnumerable<ReceiveInfo>? ReceiveList { get; set; }
    public IEnumerable<DeliveryInfo>? DeliveryList { get; set; }
    public IEnumerable<ReturnInfo>? ReturnList { get; set; }
    public IEnumerable<GoodsCoding>? GoodsCodingList { get; set; }
    public IEnumerable<DocumentList>? DocumentList { get; set; }
}
public class GetLogisticsSupplyGoods
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public long? UnitMeasurementId { get; set; }
    public string? UnitMeasurementName { get; set; }
    public long? GoodsTypeId { get; set; }
    public string? GoodsTypeName { get; set; }
    public long? GoodsCategoryId { get; set; }
    public string? GoodsCategoryName { get; set; }
    public string? Description { get; set; }
    public int? Quantity { get; set; }
    public float? ServiceDuration { get; set; }
    public int? UsageDuration { get; set; }
    public long LogisticsRequestId { get; set; }
    public string? IsContractRequired { get; set; }
    public string? IsPrePaymentRequired { get; set; }
    public int? PrePaymentPercentage { get; set; }
    public string? LogisticsRequestCode { get; set; }
    public DateTime? RequestDateTime { get; set; }
    public string? RequestDateTimePersian => RequestDateTime.ToPersianDateTime();
    public long? RequesterId { get; set; }
    public string? RequesterName { get; set; }
    public decimal? EstimatedPrice { get; set; }
    public long? GoodsStatusId { get; set; }
    public string? GoodsStatusName { get; set; }
}
public class GetPriceEstimationInfo
{
    public decimal EstimatedPrice { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
}
public class GetCandidateSupplier
{
    public long SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? IsSelected { get; set; }
    public DateTime? SelectionDate { get; set; }
    public string? SelectionDatePersian => SelectionDate.ToPersianDateTime();
    public decimal? InquieredPrice { get; set; }
    public string? IsWrittenInquiry { get; set; }
    public long? InvoiceDocumentId { get; set; }
    public string? InvoiceDocumentName { get; set; }
    public string? DocumentExtensionName { get; set; }
    public string? DocumentContentType { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
}
public class GetOrdering
{
    public long OrderingId { get; set; }
    public long SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public long? ReceiptDocumentId { get; set; }
    public string? ReceiptDocumentName { get; set; }
    public string? DocumentExtensionName { get; set; }
    public string? DocumentContentType { get; set; }
    public string? ReceiptNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? OrderDatePersian => OrderDate.ToPersianDateTime();
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public IEnumerable<GetOrderingItem>? OrderingItemList { get; set; }
}
public class GetOrderingItem
{
    public long? OrderingId { get; set; }
    public long? LogisticsSupplyGoodsId { get; set; }
    public decimal? ItemPrice { get; set; }
    public string? GoodsCategoryName { get; set; }
    public long? GoodsCategoryId { get; set; }
    public string? GoodsName { get; set; }
    public long? GoodsId { get; set; }
}
public class GetSupplierContract
{
    public long Id { get; set; }
    public long SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? ContractNumber { get; set; }
    public DateTime? ContractDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? ContractDatePersian => ContractDate.ToPersianDate();
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public long? ContractDocumentId { get; set; }
    public string? ContractDocumentName { get; set; }
    public string? DocumentExtensionName { get; set; }
    public string? DocumentContentType { get; set; }
    public string? Description { get; set; }
    public string? CreatedBy { get; set; }
}
public class GetPaymentCommand
{
    public long Id { get; set; }
    public long OrderingId { get; set; }
    public DateTime? CommandDate { get; set; }
    public string? CommandDatePersian => CommandDate.ToPersianDate();
    public string? CommandDescription { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
}
public class GetPaymentHistory
{
    public long Id { get; set; }
    public long OrderingId { get; set; }
    public long PaymentCommandId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDatePersian => PaymentDate.ToPersianDate();
    public string? PaymentNumber { get; set; }
    public decimal? PaymentValue { get; set; }
    public long? PaymentTypeId { get; set; }
    public string? PaymentTypeName { get; set; }
    public string? PaymentDescription { get; set; }
    public long? PaymentDocumentId { get; set; }
    public string? PaymnentDocumentName { get; set; }
    public string? DocumentExtensionName { get; set; }
    public string? DocumentContentType { get; set; }
}
public class GetPrepaymentCommand
{
    public long Id { get; set; }
    public DateTime? CommandDate { get; set; }
    public string? CommandDatePersian => CommandDate.ToPersianDate();
    public string? CommandDescription { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
}
public class GetPrepaymentHistory
{
    public long PaymentCommandId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDatePersian => PaymentDate.ToPersianDate();
    public string? PaymentNumber { get; set; }
    public decimal? PaymentValue { get; set; }
    public long? PaymentTypeId { get; set; }
    public string? PaymentTypeName { get; set; }
    public string? PaymentDescription { get; set; }
    public long? PaymentDocumentId { get; set; }
    public string? PaymentDocumentName { get; set; }
    public string? DocumentExtensionName { get; set; }
    public string? DocumentContentType { get; set; }
    public long? SupplyAccountListId { get; set; }
    public string? SupplyAccountListName { get; set; }

}

