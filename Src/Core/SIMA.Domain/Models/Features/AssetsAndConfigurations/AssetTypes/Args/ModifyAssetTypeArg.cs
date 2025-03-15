namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Args;

public class ModifyAssetTypeArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? ParentId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}