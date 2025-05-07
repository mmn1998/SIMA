namespace SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions
{
    public class GetWorkFlowDocumentExtensionQueryResult
    {
        public long Id { get; set; }
        public long WorkflowId { get; set; }
        public long DocumentExtensionId { get; set; }
        public string? ActiveStatus { get; set; }
        public long? ActiveStatusId { get; set; }

    }
}
