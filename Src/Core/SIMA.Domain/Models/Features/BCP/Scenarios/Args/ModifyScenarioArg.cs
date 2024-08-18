namespace SIMA.Domain.Models.Features.BCP.Scenarios.Args
{
    public class ModifyScenarioArg
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
