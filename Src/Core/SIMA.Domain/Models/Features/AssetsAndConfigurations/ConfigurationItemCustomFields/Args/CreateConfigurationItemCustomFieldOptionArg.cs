namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;

public class CreateConfigurationItemCustomFieldOptionArg
{
    public long Id { get; set; }
    public long? ConfigurationItemId { get; set; }
    public string? OptionValue { get; set; }
    public string? OptionText { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}