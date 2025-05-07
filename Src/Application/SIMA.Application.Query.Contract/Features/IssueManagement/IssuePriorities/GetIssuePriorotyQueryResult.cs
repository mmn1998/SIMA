namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;

public class GetIssuePriorotyQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float Ordering { get; set; }
    public string? ActiveStatus { get; set; }
    public long? ActiveStatusId { get; set; }
}
