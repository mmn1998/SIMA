namespace SIMA.Domain.Models.Features.RiskManagement.Threats.Args
{
    public class ModifyThreatArg
    {
        public string Code { get; set; }
        public long RiskId { get; set; }
        public long RiskPossibilityId { get; set; }
        public long ThreatTypeId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
