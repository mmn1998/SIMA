namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args;

public class ModifyIssueLinkArg
{
    public long Id { get; private set; }
    public long IssueId { get; private set; }
    public long IssueIdLinkedTo { get; private set; }
    public long IssueLinkReasonTo { get; private set; }
    public long ActiveStatusId { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
