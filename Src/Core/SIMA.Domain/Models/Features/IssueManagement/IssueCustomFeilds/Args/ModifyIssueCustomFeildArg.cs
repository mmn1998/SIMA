namespace SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Args;

public class ModifyIssueCustomFeildArg
{
    public long IssueId { get; set; }
    public long CustomeFeildId { get; set; }
    public string KeyValues { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }

}
