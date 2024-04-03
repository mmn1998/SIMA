namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;

public class ModifyApprovalSupervisorAnswerDocumentArg
{
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
