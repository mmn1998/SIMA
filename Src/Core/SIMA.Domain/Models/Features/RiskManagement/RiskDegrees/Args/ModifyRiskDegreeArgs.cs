namespace SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Args
{
    public class ModifyRiskDegreeArgs
    {
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Code { get;  set; }
        public float Degree { get;  set; }
        public string Color { get;  set; }
        public string IsImportantBia { get; set; }
        public long ActiveStatusId { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
