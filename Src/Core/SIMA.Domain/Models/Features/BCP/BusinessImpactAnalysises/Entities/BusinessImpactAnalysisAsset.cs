using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;

public class BusinessImpactAnalysisAsset : Entity
{
    private BusinessImpactAnalysisAsset()
    {

    }
    private BusinessImpactAnalysisAsset(CreateBusinessImpactAnalysisAssetArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        //AssetId = new(arg.AssetId);
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessImpactAnalysisAsset Create(CreateBusinessImpactAnalysisAssetArg arg)
    {
        return new BusinessImpactAnalysisAsset(arg);
    }
    public void Modify(ModifyBusinessImpactAnalysisAssetArg arg)
    {
        //AssetId = new(arg.AssetId);
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public BusinessImpactAnalysisAssetId Id { get; private set; }
    /// TODO : AssetId
    //public AssetId AssetId { get; private set; }
    //public virtual Asset Asset { get; private set; }
    public BusinessImpactAnalysisId BusinessImpactAnalysisId { get; private set; }
    public virtual BusinessImpactAnalysis BusinessImpactAnalysis { get; private set; }
    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
