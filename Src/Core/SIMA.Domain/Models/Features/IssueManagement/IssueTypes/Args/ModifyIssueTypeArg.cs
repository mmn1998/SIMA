namespace SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Args;

public class ModifyIssueTypeArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string IconPath { get; set; }
    public string ColorHex { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }

}
