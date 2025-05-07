using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;

public class AssetChangeTechnicalStatusHistory : Entity
{
    private AssetChangeTechnicalStatusHistory()
    {

    }
    private AssetChangeTechnicalStatusHistory(CreateAssetChangeTechnicalStatusHistoryArg arg)
    {
        Id = new(arg.Id);
        AssetId = new(arg.AssetId);
        FromAssetTechnicalStatusId = new(arg.FromAssetTechnicalStatusId);
        ToAssetTechnicalStatusId = new(arg.ToAssetTechnicalStatusId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static AssetChangeTechnicalStatusHistory Create(CreateAssetChangeTechnicalStatusHistoryArg arg)
    {
        return new AssetChangeTechnicalStatusHistory(arg);
    }
    public AssetChangeTechnicalStatusHistoryId Id { get; private set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public AssetTechnicalStatusId FromAssetTechnicalStatusId { get; private set; }
    public virtual AssetTechnicalStatus FromTechnicalStatus { get; private set; }
    public AssetTechnicalStatusId ToAssetTechnicalStatusId { get; private set; }
    public virtual AssetTechnicalStatus ToTechnicalStatus { get; private set; }
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

