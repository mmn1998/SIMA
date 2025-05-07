namespace SIMA.Domain.Models.Features.BCP.Scenarios.Args;

public class CreateScenarioBusinessContinuityPlanVersioningArg
{
    public long Id { get; set; }
    public long BusinessContinuityPlanVersioningId { get; set; }
    public long ScenarioId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
