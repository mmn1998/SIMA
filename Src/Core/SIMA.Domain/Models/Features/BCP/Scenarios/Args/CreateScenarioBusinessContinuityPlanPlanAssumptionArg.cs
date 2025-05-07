namespace SIMA.Domain.Models.Features.BCP.Scenarios.Args;

public class CreateScenarioBusinessContinuityPlanAssumptionArg
{
    public long Id { get; set; }
    public long BusinessContinuityPlanScenarioCobitScenarioId { get; set; }
    public long BusinessContinuityPlanAssumptionId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}