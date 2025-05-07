namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args
{
    public class ModifyPreventiveActionArg
    {
        public long RiskId { get; set; }
        public string ActionDescription { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
