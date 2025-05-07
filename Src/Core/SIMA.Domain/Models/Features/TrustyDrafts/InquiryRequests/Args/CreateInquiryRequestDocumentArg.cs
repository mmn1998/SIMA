namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Args;

public class CreateInquiryRequestDocumentArg
{
    public long Id { get; set; }
    public long InquiryRequestId { get; set; }
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
