namespace SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Args
{
    public class CreateThreatTypeArg
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
