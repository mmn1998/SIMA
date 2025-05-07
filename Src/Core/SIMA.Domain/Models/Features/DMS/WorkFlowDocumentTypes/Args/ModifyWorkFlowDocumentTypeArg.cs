namespace SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Args;

public class ModifyWorkFlowDocumentTypeArg
{
    public long Id { get; set; }
    public long WorkflowId { get; set; }
    public long DocumentTypeId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
