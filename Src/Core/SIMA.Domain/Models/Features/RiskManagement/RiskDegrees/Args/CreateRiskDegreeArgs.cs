namespace SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Args
{
    public class CreateRiskDegreeArgs
    {
        public string Name { get;  set; }
        public string Code { get;  set; }
        public float Degree { get;  set; }
        public string Color { get;  set; }
        public string IsImportantBia { get; set; }
        public long ActiveStatusId { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public long? CreatedBy { get;  set; }
    }
}
