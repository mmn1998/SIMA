namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class CreateBusinessImpactAnalysisAssetArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long BusinessImpactAnalysisId { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long CreatedBy { get; set; }
}