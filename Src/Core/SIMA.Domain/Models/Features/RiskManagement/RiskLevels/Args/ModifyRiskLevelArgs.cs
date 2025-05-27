namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Args
{
    public class ModifyRiskLevelArgs
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long RiskValueId { get; set; }
        public long SeverityValueId { get; set; }
        public long CurrentOccurrenceProbabilityValueId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
