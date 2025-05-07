namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;

public class ModifyAssetCustomFieldValueArg
{
    public long Id { get; set; }
    public long AssetCustomFieldId { get; set; }
    public long AssetId { get; set; }
    public string ItemValue { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}