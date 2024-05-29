namespace SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Args
{
    public class CreateRiskPossibilityArgs
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float Possibility { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
