namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Args;

public class ModifyInquiryRequestArg
{
    public long Id { get; set; }
    public string? BeneficiaryName { get; set; }
    public string? ReferenceNumber { get; set; }
    public long PaymentTypeId { get; set; }
    public long CustomerId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}
