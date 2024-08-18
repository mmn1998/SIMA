namespace SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Args
{
    public class CreateScenarioRecoveryCriteriaArg
    {
        public long Id { get; set; }
        public long ScenarioId { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
