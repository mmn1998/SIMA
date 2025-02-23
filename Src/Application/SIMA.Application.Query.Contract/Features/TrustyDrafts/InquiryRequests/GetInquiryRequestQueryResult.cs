using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests;

public class GetInquiryRequestQueryResult
{
    public long InquiryRequestId { get; set; }
    public string? BeneficiaryName { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? DraftOrderNumber { get; set; }
    public string? ProformaNumber { get; set; }
    public string? RequestDescription { get; set; }    
    public string? PaymentTypeName { get; set; }
    public long? PaymentTypeId { get; set; }    
    public string? CustomerName { get; set; }
    public long? CustomerId { get; set; }
    public long? BranchId { get; set; }
    public string? BranchName { get; set; }
    public string? BranchCode { get; set; }
    public DateTime RequestCreatedAt { get; set; }
    public string RequestCreatedAtPersian => DateHelper.ToPersianDateTime(RequestCreatedAt);
    public string? RequestCreatedBy { get; set; }
    public long? InquiryResponseId { get; set; }
    public long? BrokerInquiryStatusId { get; set; }
    public string? BrokerInquiryStatusName { get; set; }
    public long? BrokerId { get; set; }
    public string? BrokerName { get; set; }
    public string? ResponseDescription { get; set; }
    public long? WageRateId { get; set; }
    public string? WageRateName { get; set; }
    public decimal? CalculatedWage { get; set; }
    public decimal? ExcessWage { get; set; }
    public DateTime? ResponseCreatedAt { get; set; }
    public string? ResponseCreatedAtPersian => DateHelper.ToPersianDateTime(ResponseCreatedAt);
    public DateTime? ValidityPeriod { get; set; }
    public string? ValidityPeriodPersian => ValidityPeriod.ToPersianDateTime();
    public string? ResponseCreatedBy { get; set; }
    public decimal? Amount { get; set; }
    public long? CurrencyTypeId { get; set; }
    public string? CurrencyTypeName { get; set; }
    public IEnumerable<GetInquiryRequestDocumentQueryResult>? InquiryRquestDocumentList { get; set; }
    public IEnumerable<GetInquiryRequestCurrencyQueryResult>? InquiryRquestCurrencyList { get; set; }
    public long? DraftOriginId { get; set; }
    public string? DraftOriginName { get; set; }
    public DateTime? DraftOrderDate { get; set; }
    public string? DraftOrderDatePersian => DraftOrderDate.ToPersianDateTime();
    public DateTime? ProformaDate { get; set; }
    public string? ProformaDatePersian => ProformaDate.ToPersianDateTime();
    public decimal? ProformaAmount { get; set; }
    public long? ProformaCurrencyTypeId { get; set; }
    public string? ProformaCurrencyTypeName { get; set; }
    public long? ExcessWageCurrencyTypeId { get; set; }
    public string? ExcessWageCurrencyTypeName { get; set; }
}
public class GetInquiryRequestDocumentQueryResult
{
    public long Id { get; set; }
    public long DocumentId { get; set; }
    public long DocumentTypeId { get; set; }
    public string? DocumentTypeName { get; set; }
    public string? DocumentFileName { get; set; }
    public long FileExtensionId { get; set; }
    public string? DocumentContentType { get; set; }
    public string? DocumentExtensionName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? AttachStepName { get; set; }
}
public class GetInquiryRequestCurrencyQueryResult
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string? CurrencyTypeName { get; set; }
    public long CurrencyTypeId { get; set; }
}