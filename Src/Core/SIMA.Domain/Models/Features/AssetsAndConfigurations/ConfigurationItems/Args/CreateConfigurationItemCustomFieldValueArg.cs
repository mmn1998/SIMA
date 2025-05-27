namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemCustomFieldValueArg
{
    public long Id { get; set; }
    public long ConfigurationItemId { get; set; }
    public long AssetId { get; set; }
    public string ItemValue { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}