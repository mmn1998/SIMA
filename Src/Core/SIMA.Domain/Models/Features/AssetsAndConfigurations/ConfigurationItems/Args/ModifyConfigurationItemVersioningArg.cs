namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class ModifyConfigurationItemVersioningArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public string? VersionNumber { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public long ConfigurationItemId { get; set; }
    public long? ModifiedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
}

