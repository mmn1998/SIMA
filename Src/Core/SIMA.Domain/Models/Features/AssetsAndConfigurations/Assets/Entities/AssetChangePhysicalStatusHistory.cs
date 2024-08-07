using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;

public class AssetChangePhysicalStatusHistory : Entity
{
    private AssetChangePhysicalStatusHistory() { }
    private AssetChangePhysicalStatusHistory(CreateAssetChangePhysicalStatusHistoryArg arg)
    {
        Id = new(arg.Id);
        AssetId = new(arg.AssetId);
        FromAssetPhysicalStatusId = new(arg.FromAssetPhysicalStatusId);
        ToAssetPhysicalStatusId = new(arg.ToAssetPhysicalStatusId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public AssetChangePhysicalStatusHistoryId Id { get; private set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public AssetPhysicalStatusId FromAssetPhysicalStatusId { get; private set; }
    public virtual AssetPhysicalStatus FromAssetPhysicalStatus { get; private set; }
    public AssetPhysicalStatusId ToAssetPhysicalStatusId { get; private set; }
    public virtual AssetPhysicalStatus ToAssetPhysicalStatus { get; private set; }
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

