namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Args;

public class CreateInquiryRequestArg
{
    public long Id { get; set; }
    public string BeneficiaryName { get; set; }
    public string ReferenceNumber { get; set; }
    public string DraftOrderNumber { get; set; }
    public long DraftOriginId { get; set; }
    public long? ProformaCurrencyTypeId { get; set; }
    public DateTime? DraftOrderDate { get; set; }
    public DateTime? ProformaDate { get; set; }
    public decimal? ProformaAmount { get; set; }
    public string? ProformaNumber { get; set; }
    public string? Description { get; set; }
    public long PaymentTypeId { get; set; }
    public long CustomerId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
