namespace SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Args;

public class ModifyIssueLinkReasonArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }

}
