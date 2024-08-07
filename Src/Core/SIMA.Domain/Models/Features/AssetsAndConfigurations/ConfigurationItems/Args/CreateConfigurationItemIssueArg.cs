namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemIssueArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public long ConfigurationItemVersionId { get; set; }
    public long IssueId { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}

