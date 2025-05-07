namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Args;

public class ModifyConfigurationItemRelationshipTypeArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long? ModifiedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
}
