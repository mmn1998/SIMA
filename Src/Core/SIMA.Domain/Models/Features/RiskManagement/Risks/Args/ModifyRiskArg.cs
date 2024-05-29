namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args
{
    public class ModifyRiskArg
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long RiskTypeId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
