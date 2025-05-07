namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;

public class CreateConfigurationItemCustomFieldOptionCommand
{
    public long ConfigurationItemCustomFieldId { get; set; }
    public string? OptionValue { get; set; }
    public string? OptionText { get; set; }
}
