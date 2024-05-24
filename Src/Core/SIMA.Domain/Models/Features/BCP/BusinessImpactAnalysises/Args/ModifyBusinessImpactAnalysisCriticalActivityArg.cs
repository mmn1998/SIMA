namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class ModifyBusinessImpactAnalysisCriticalActivityArg
{
    public long BusinessImpactAnalysisId { get; set; }
    public long? CriticalActivityId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
