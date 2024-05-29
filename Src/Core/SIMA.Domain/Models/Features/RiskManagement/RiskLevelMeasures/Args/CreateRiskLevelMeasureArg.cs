namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Args
{
    public class CreateRiskLevelMeasureArg
    {
        public string Code { get; set; }
        public long RiskLevelId { get; set; }
        public long RiskPossibilityId { get; set; }
        public long RiskImpactId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
