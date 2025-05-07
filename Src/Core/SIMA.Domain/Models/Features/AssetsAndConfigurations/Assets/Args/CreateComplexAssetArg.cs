namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;

public class CreateComplexAssetArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long ParentAssetId { get; set; }
    public long ActiveStatusId { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}

