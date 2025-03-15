namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemApiArg
{
    public long Id { get; set; }
    public long ConfigurationItemId { get; set; }
    public long ApiId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}