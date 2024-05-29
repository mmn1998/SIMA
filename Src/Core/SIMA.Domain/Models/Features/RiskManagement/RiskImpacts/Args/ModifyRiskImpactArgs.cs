namespace SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Args
{
    public class ModifyRiskImpactArgs
    {
        public string Name { get;  set; }
        public string Code { get;  set; }
        public float Impact { get;  set; }
        public long ActiveStatusId { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
