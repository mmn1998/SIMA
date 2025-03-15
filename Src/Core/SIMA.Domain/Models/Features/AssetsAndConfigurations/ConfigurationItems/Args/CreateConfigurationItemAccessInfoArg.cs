namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemAccessInfoArg
{
    public long Id { get; set; }
    public long ConfigurationItemId { get; set; }
    public string? IPAddressFrom { get; set; }
    public string? IPAddressTo { get; set; }
    public string? PortFrom { get; set; }
    public string? PortTo { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime ActiveFrom { get; set; }
    public DateTime? ActiveTo { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}