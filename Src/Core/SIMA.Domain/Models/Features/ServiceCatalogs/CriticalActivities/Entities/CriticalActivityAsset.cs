using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivityAsset : Entity
{
    private CriticalActivityAsset() { }
    private CriticalActivityAsset(CreateCriticalActivityAssetArg arg)
    {
        Id = new CriticalActivityAssetId(arg.Id);
        CriticalActivityId = new CriticalActivityId(arg.CriticalActivityId);
        AssetId = new AssetId(arg.AssetId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static CriticalActivityAsset Create(CreateCriticalActivityAssetArg arg)
    {
        return new CriticalActivityAsset(arg);
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }

    public CriticalActivityAssetId Id { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
