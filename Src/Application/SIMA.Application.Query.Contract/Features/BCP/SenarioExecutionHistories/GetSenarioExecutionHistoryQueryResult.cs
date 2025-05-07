namespace SIMA.Application.Query.Contract.Features.BCP.SenarioExecutionHistories
{
    public class GetSenarioExecutionHistoryQueryResult
    {
        public long Id { get; set; }
        public long ScenarioId { get; set; }
        public string? scenarioCode { get; set; }
        public string? scenariotitle { get; set; }
        public int ExecutionNumber { get; set; }
        public DateTime ExecutionDate { get; set; }
        public TimeSpan ExectionTimeFrom { get; set; }
        public TimeSpan ExectionTimeTo { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}


