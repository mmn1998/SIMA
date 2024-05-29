namespace SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Args
{
    public class CreateRiskImpactArgs
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float Impact { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
