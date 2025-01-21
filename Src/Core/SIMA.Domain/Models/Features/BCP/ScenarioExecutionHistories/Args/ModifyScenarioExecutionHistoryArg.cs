namespace SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Args
{
    public class ModifyScenarioExecutionHistoryArg
    {
        public long Id { get; set; }
        public long ScenarioId { get; set; }
        public long ExecutionNumber { get; set; }
        public DateTime ExecutionDate { get; set; }
        public TimeOnly? ExecutionTimeFrom { get; set; }
        public TimeOnly? ExecutionTimeTo { get; set; }
        public long ActiveStatusId { get; set; }
        public string? Description { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
