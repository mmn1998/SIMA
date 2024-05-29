namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceRelatedIssueArg
{
    public long Id { get; private set; }
    public long ServiceId { get; private set; }
    public long IssueId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
}
