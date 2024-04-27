namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetIssueDocumentQueryResult
{
    public long DocumentId { get; set; }
    public string DocumentPath { get; set; }
    public string DocumentExtentionName { get; set; }
}
