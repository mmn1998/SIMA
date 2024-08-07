namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args;

public class CreateIssueLinkArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long? IssueIdLinkedTo { get; set; }
    public long? IssueLinkReasonTo { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
