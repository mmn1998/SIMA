namespace SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Args;

public class CreateIssueWeightCategoryArg
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int MinRange { get; set; }
    public int MaxRange { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }

}
