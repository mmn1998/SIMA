namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;

public class CreateApprovalResponsibleAnswerDocumentArg
{
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}