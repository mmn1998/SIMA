namespace SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Args
{
    public class CreateRiskCriteriaArg
    {
        public string Code { get; set; }
        public long RiskDegreeId { get; set; }
        public long RiskPossibilityId { get; set; }
        public long RiskImpactId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
