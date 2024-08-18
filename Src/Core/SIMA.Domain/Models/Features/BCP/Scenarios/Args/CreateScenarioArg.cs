namespace SIMA.Domain.Models.Features.BCP.Scenarios.Args
{
    public class CreateScenarioArg
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
