namespace SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Args
{
    public class ModifyServiceRiskImpactArg
    {
        public long ImpactScaleId { get;  set; }
        public long RiskImpactId { get;  set; }
        public long ActiveStatusId { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
