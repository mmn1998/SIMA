namespace SIMA.Application.Contract.Features.BCP.BusinesImpactAnalysises;

public class CreateBusinessImpactAnalysisDisasterOriginCommand
{
    public long OriginId { get; set; }
    //public long HappeningPossibilityId { get; set; }
    public long ConsequenceId { get; set; }
    public long RecoveryPointObjectivesId { get; set; }
    public long TimeMeasurementId { get; set; }
    public float? RTO { get; set; }
    public float? RPO { get; set; }
    public float? WRT { get; set; }
    public float? MTD { get; set; }
}

