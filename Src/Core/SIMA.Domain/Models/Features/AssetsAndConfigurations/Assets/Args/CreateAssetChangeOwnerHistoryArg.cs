namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;

public class CreateAssetChangeOwnerHistoryArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long? FromOwnerId { get; set; }
    public long? ToOwnerId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}

