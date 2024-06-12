using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args;

public class ModifyIssueApprovalArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long StepApprovalOptionId { get; set; }
    public long ApprovedBy { get; set; }
    public long WorkflowActorId { get; set; }
    public string Description { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
