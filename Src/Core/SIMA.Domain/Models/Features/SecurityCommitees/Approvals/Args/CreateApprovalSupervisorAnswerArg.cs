namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;

public class CreateApprovalSupervisorAnswerArg
{
    public long ApprovalId { get; set; }
    public long SupervisorAnswerTypeId { get; set; }
    public string? Description { get; set; }
    public DateTime ReportDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}