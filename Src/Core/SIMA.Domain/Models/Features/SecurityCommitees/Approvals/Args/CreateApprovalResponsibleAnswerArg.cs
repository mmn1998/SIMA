namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;

public class CreateApprovalResponsibleAnswerArg
{
    public long ApprovalId { get; set; }
    public long ResponsibleAnswerTypeId { get; set; }
    public string? Description { get; set; }
    public DateTime ReportDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}