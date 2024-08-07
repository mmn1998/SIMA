namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemAssetHistoryArg
{
    public long Id { get; set; }
    public long ConfigurationItemVersioningId { get; set; }
    public long AssetId { get; set; }
    public string? IsAssigned { get; set; }
    public DateOnly? AssignDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
