namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemChangeStatusHistoryArg
{
    public long Id { get; set; }
    public long FromConfigurationItemStatusId { get; set; }
    public long ToConfigurationItemStatusId { get; set; }
    public long ConfigurationItemId { get; set; }
    public long ActiveStatusId { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}
