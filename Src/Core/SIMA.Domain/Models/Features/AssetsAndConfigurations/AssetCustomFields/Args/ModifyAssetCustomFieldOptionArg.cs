namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;

public class ModifyAssetCustomFieldOptionArg
{
    public long Id { get; set; }
    public long? AssetCustomFieldId { get; set; }
    public string? OptionValue { get; set; }
    public string? OptionText { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}