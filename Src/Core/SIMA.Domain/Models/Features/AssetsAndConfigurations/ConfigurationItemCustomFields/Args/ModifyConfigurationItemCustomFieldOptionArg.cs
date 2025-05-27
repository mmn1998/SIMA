namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;

public class ModifyConfigurationItemCustomFieldOptionArg
{
    public long Id { get; set; }
    public long? ConfigurationItemCustomFieldId { get; set; }
    public string? OptionValue { get; set; }
    public string? OptionText { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}