namespace SIMA.Domain.Models.Features.BCP.Scenarios.Args
{
    public class ModifyScenarioBusinessContinuityPlanVersioningArg
    {
        public long Id { get; set; }
        public long BusinessContinuityPlanVersioningId { get; set; }
        public long ScenarioId { get; set; }
        public long ActiveStatusId { get; set; }
        public float Ordering { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
