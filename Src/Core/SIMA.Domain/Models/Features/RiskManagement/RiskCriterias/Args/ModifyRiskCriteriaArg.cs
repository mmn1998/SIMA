namespace SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Args
{
    public class ModifyRiskCriteriaArg
    {
        public long Id { get; set; }
        public string Code { get;  set; }
        public long RiskDegreeId { get;  set; }
        public long RiskPossibilityId { get;  set; }
        public long RiskImpactId { get;  set; }
        public long ActiveStatusId { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
