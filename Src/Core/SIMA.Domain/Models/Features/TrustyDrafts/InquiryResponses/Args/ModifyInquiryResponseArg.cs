namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Args;

public class ModifyInquiryResponseArg
{
    public long Id { get; set; }
    public long BrokerInquiryStatusId { get; set; }
    public long InquiryRequestId { get; set; }
    public long BrokerId { get; set; }
    public long WageRateId { get; set; }
    public decimal CalculatedWage { get; set; }
    public decimal ExcessWage { get; set; }
    public DateTime ValidityPeriod { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
