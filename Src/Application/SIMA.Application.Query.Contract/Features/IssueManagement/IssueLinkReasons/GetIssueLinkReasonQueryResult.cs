namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons
{
    public class GetIssueLinkReasonQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? ActiveStatus { get; set; }
        public long? ActiveStatusId { get; set; }
    }
}
