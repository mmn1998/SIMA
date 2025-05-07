namespace SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentTypes;

public class GetWorkFlowDocumentTypeQueryResult
{
    public long Id { get; set; }
    public long WorkflowId { get; set; }
    public long DocumentTypeId { get; set; }
    public string? ActiveStatus { get; set; }
    public long? ActiveStatusId { get; set; }
}
