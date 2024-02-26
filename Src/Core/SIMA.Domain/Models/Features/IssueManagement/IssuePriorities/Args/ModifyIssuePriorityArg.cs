namespace SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Args;

public class ModifyIssuePriorityArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public float Ordering { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
