namespace SIMA.Application.Contract.Features.BCP.BusinesImpactAnalysises;

public class CreateBusinessImpactAnalysisDisasterOriginCommand
{
    public long OriginId { get; set; }
    //public long HappeningPossibilityId { get; set; }
    public long ConsequenceId { get; set; }
    public long ConsequenceIntensionId { get; set; }
}

 