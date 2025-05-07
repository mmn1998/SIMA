namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class AddDocumentToSP
    {
        public long Id { get; set; }
        public long SourceId { get; set; }
        public long DocumentId { get; set; }
        public long ActiveStatusId { get; set; }
        public long CreatedBy { get; set; }
        public long MainAggregateId { get; set; }
    }
}
