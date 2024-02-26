namespace SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Args;

public class ModifyIssueWeightCategoryArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int MinRange { get; set; }
    public int MaxRange { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }

}
