namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args;

public class CreateIssueManagerArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long UserId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
