namespace SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Args;

public class ModifyWorkFlowDocumentExtensionArg
{
    public long Id { get; set; }
    public long WorkflowId { get; set; }
    public long DocumentExtensionId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
