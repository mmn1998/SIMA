namespace SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Args;

public class CreateIssueApprovalArg
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string IsApproval { get; set; }
    public long WorkflowStepId { get; set; }
    public long WorkflowActorId { get; set; }
    public string Description { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
