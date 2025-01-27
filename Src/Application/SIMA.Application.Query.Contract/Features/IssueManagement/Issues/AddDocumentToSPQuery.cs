namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class AddDocumentToSPQuery
    {
        public string Id { get; set; }
        public string SourceId { get; set; }
        public string DocumentId { get; set; }
        public string ActiveStatusId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
    }
}
