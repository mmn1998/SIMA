namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class CreateBusinessImpactAnalysisArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public long ServiceId { get; set; }
    public long RecoveryPointObjectiveId { get; set; }
    public long TimeMeasurementId { get; set; }
    public long RTO { get; set; }
    public long WRT { get; set; }
    public long MTPD { get; set; }
}