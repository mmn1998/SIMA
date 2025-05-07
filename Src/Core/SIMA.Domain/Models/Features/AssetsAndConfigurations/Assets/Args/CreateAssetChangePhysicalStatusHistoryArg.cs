namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;

public class CreateAssetChangePhysicalStatusHistoryArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long FromAssetPhysicalStatusId { get; set; }
    public long ToAssetPhysicalStatusId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}

