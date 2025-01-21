using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryResponses;

public class GetInquiryResponseQueryResult
{
    public long Id { get; set; }
    public decimal? ExcessWage { get; set; }
    public DateTime? ValidityPeriod { get; set; }
    public string? ValidityPeriodPersian => ValidityPeriod.ToPersianDateTime();
    public decimal CalculatedWage { get; set; }
    public long InquiryRequestId { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? BeneficiaryName { get; set; }
    public decimal Amount { get; set; }
    public long BrokerInquiryStatusId { get; set; }
    public string? BrokerInquiryStatusName { get; set; }
    public long BrokerId { get; set; }
    public string? BrokerName { get; set; }
    public long WageRateId { get; set; }
    public string? WageRateName { get; set; }
    public string? WageRateDescription { get; set; }
    public decimal WageRateCurrentBalance { get; set; }
    public int WageRateVagePrecentage { get; set; }
    public string? ActiveStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
    public string? CreatedBy { get; set; }
    public string? DraftNumber { get; set; }
}
