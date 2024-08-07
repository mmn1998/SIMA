namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Args;

public class ModifyAssetTechnicalStatusArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
