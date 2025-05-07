using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.Auths.Warehouses.Entities;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;

public class AssetWarehouseHistory : Entity
{
    private AssetWarehouseHistory() { }
    private AssetWarehouseHistory(CreateAssetWarehouseHistoryArg arg)
    {
        Id = new(arg.Id);
        AssetId = new(arg.AssetId);
        WarehouseId = new(arg.WarehouseId);
        IsCheckIn = arg.IsCheckIn;
        CheckDate = arg.CheckDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public AssetWarehouseHistoryId Id { get; private set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public WarehouseId WarehouseId { get; private set; }
    public virtual Warehouse Warehouse { get; private set; }
    public string? IsCheckIn { get; private set; }
    public DateOnly CheckDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}

