namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;

public class CreateConfigurationItemAccessInfoCommand
{
    public string? IPAddressFrom { get; set; }
    public string? IPAddressTo { get; set; }
    public string? PortFrom { get; set; }
    public string? PortTo { get; set; }
    public string? ActiveFrom { get; set; }
    public string? ActiveTo { get; set; }
}
