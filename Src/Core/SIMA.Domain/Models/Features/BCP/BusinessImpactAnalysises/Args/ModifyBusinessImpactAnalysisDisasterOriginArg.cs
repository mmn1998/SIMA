namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class ModifyBusinessImpactAnalysisDisasterOriginArg
{
    public long BusinessImpactAnalysisId { get; set; }
    public long HappeningPossibilityId { get; set; }
    public long ConsequenceId { get; set; }
    public long RecoveryPointObjectiveId { get; set; }
    public string? Description { get; set; }
    public float? RTO { get; set; }
    public float? RPO { get; set; }
    public float? WRT { get; set; }
    public float? MTD { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
