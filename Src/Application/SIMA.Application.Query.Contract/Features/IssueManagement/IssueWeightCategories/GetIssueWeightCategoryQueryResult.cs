namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;

public class GetIssueWeightCategoryQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int? MinRange { get; set; }
    public int? MaxRange { get; set; }
    public string? ActiveStatus { get; set; }
    public long? ActiveStatusId { get; set; }
}
