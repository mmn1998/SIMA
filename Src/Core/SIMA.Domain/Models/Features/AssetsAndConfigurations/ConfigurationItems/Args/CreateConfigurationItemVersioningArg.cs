namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemVersioningArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public string? VersionNumber { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public long ConfigurationItemId { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}
