namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetStepRequiredDocumentQueryResult
{
    public long DocumentTypeId { get; set; }
    public string DocumentTypeName { get; set; }
    public int Count { get; set; }
}
