namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class CreateBusinessImpactAnalysisDisasterOriginArg
{
    public long Id { get; set; }
    public long BusinessImpactAnalysisId { get; set; }
    public long ConsequenceId { get; set; }
    public long OriginId { get; set; }
    public long ConsequenceIntensionId { get; set; }   
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}