namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemRelationshipArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public long RelatedConfigurationItemVersioningId { get; set; }
    public long ConfigurationItemVersioningId { get; set; }
    public long ConfigurationItemRelationshipTypeId { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}
