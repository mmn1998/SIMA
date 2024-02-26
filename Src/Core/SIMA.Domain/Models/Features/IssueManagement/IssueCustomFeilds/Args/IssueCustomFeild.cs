namespace SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Args;

public class CreateIssueCustomFeildArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long CustomeFeildId { get; set; }
    public string KeyValues { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }

}
