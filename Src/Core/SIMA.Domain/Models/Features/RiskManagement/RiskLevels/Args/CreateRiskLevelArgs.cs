namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Args
{
    public class CreateRiskLevelArgs
    {
        public string Code { get; set; }
        public long RiskValueId { get; set; }
        public long SeverityValueId { get; set; }
        public long CurrentOccurrenceProbabilityValueId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
