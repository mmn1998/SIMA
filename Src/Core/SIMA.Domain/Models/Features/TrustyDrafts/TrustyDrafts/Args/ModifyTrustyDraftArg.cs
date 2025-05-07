namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;

public class ModifyTrustyDraftArg
{
    public long Id { get; set; }
    public long? InquiryRequestId { get; set; }
    public long WageDeductionMethodId { get; set; }
    public long IssueId { get; set; }
    public string DraftNumber { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
    public long DraftTypeId { get; set; }
    public decimal DraftIssueWage { get; set; }
}
