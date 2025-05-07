namespace SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Args;

public class CreateWorkflowDocumentExtensionArg
{
    public long WorkflowId { get; set; }
    public long DocumentExtensionId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
