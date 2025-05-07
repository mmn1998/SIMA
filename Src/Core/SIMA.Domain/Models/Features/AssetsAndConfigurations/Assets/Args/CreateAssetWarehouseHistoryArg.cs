namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;

public class CreateAssetWarehouseHistoryArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long WarehouseId { get; set; }
    public string? IsCheckIn { get; set; }
    public DateOnly CheckDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}

