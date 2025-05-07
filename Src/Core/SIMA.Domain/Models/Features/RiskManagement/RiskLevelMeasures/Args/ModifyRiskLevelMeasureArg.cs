namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Args
{
    public class ModifyRiskLevelMeasureArg
    {
        public long Id { get; set; }
        public string Code { get;  set; }
        public long RiskLevelId { get;  set; }
        public long RiskPossibilityId { get;  set; }
        public long RiskImpactId { get;  set; }
        public long ActiveStatusId { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
