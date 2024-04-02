namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args;

public class ModifyIssueArg
{
    public long Id { get; set; }
    public long IssueTypeId { get; set; }
    public string Description { get; set; }
    public string Summery { get; set; }
    public long IssuePriorityId { get; set; }
    public long IssueWeightCategoryd { get; set; }
    public int Weight { get; set; }
    public DateTime DueDate { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }

}