namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args;

public class ModifyIssueDocumentArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
