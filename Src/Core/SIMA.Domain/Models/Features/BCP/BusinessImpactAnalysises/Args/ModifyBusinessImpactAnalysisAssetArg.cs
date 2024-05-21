namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class ModifyBusinessImpactAnalysisAssetArg
{
    public long AssetId { get; set; }
    public long BusinessImpactAnalysisId { get; set; }
    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}