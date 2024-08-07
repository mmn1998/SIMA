namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemChangeOwnerHistoryArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public long ConfigurationItemId { get; set; }
    public long FromOwnerId { get; set; }
    public long ToOwnerId { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}
