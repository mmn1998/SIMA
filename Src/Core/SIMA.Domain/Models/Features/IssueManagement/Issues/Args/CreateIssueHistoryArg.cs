namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args;

public class CreateIssueHistoryArg
{

    public long Id { get; set; }
    public string Name { get; set; }
    public long IssueId { get; set; }
    public long? SourceStateId { get; set; }
    public long? TargetStateId { get; set; }
    public long SourceStepId { get; set; }
    public long? TargetStepId { get; set; }
    public long PerformerUserId { get; set; }
    public string Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
