namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemAccessInfoArg
{
    public long Id { get; set; }
    public long ConfigurationItemVersioningId { get; set; }
    public string? IPAddress { get; set; }
    public string? Port { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime ActiveFrom { get; set; }
    public DateTime? ActiveTo { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}