namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;

public class ModifyApprovalSupervisorAnswerArg
{
    public long ApprovalId { get; set; }
    public long SupervisorAnswerTypeId { get; set; }
    public string? Description { get; set; }
    public DateTime ReportDate { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
