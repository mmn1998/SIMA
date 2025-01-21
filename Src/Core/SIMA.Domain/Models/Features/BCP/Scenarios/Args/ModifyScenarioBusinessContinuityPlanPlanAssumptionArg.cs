namespace SIMA.Domain.Models.Features.BCP.Scenarios.Args
{
    public class ModifyScenarioBusinessContinuityPlanAssumptionArg
    {
        public long Id { get; set; }
        public long BusinessContinuityPlanAssumptionId { get; set; }
        public long ScenarioId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
