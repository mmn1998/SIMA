namespace SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Args
{
    public class CreateRiskTypeArgs
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
