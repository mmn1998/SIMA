using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivityAsset : Entity
{
    private CriticalActivityAsset() { }
    private CriticalActivityAsset(CreateCriticalActivityAssetArg arg)
    {
        Id = new CriticalActivityAssetId(arg.Id);
        CriticalActivityId = new CriticalActivityId(arg.CriticalActivityId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<CriticalActivityAsset> Create(CreateCriticalActivityAssetArg arg)
    {
        return new CriticalActivityAsset(arg);
    }
    public CriticalActivityAssetId Id { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    /// <summary>
    /// TODO : assetId
    /// </summary>
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
