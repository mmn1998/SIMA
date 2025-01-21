namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;

public class CreateTrustyDraftIssueArg
{
    public long Id { get; set; }
    public long TrustyDraftId { get; set; }
    public long DraftIssueTypeId { get; set; }
    public long? ReconsilationId { get; set; }
    public long IssueId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
