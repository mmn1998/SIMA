namespace SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Args;

public class CreateWorkFlowDocumentTypeArg
{
    public long WorkflowId { get; set; }
    public long DocumentTypeId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}