namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;

public class CreateAssetCustomFieldOptionArg
{
    public long Id { get; set; }
    public long AssetCustomFieldId { get; set; }
    public string? OptionValue { get; set; }
    public string? OptionText { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}