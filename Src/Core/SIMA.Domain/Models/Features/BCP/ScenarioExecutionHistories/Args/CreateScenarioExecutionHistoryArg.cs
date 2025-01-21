namespace SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Args;

public class CreateScenarioExecutionHistoryArg
{
    public long Id { get; set; }
    public long ScenarioId { get; set; }
    public DateTime ExecutionDate { get; set; }
    public long ExecutionNumber { get; set; }
    public TimeOnly? ExecutionTimeFrom { get; set; }
    public TimeOnly? ExecutionTimeTo { get; set; }
    public string? Description { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
