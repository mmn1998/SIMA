using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Args;

public class ModifyIssueApprovalArg
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string IsApproval { get; set; }
    public long WorkflowStepId { get; set; }
    public long WorkflowActorId { get; set; }
    public string Description { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
