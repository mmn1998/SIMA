namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;

public class CreateAssetChangeTechnicalStatusHistoryArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long FromAssetTechnicalStatusId { get; set; }
    public long ToAssetTechnicalStatusId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}

