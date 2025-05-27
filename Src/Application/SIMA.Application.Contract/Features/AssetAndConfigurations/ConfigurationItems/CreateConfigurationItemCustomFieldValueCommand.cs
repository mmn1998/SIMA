namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;

public class CreateConfigurationItemCustomFieldValueCommand
{
    public long AssetId { get; set; }
    public string? Value { get; set; }
}
