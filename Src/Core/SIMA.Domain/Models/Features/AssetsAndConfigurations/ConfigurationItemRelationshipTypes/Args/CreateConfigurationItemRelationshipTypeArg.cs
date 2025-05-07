namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Args;

public class CreateConfigurationItemRelationshipTypeArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}