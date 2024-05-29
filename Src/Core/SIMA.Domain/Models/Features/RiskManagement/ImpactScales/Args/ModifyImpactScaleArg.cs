namespace SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Args
{
    public class ModifyImpactScaleArg
    {
        public string Name { get;  set; }
        public string Code { get;  set; }
        public long ActiveStatusId { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
