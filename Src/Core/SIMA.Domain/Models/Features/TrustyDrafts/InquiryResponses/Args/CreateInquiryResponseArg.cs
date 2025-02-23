namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Args;

public class CreateInquiryResponseArg
{
    public long Id { get; set; }
    public long BrokerInquiryStatusId { get; set; }
    public long InquiryRequestCurrencyId { get; set; }
    public long InquiryRequestId { get; set; }
    public long? ExcessWageCurrencyTypeId { get; set; }
    public long BrokerId { get; set; }
    public long WageRateId { get; set; }
    public decimal CalculatedWage { get; set; }
    public decimal? ExcessWage { get; set; }
    public string? Description { get; set; }
    public DateTime ValidityPeriod { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
