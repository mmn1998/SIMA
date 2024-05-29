namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Args
{
    public class CreateRiskLevelArgs
    {
        public string Name { get;  set; }
        public string Code { get;  set; }
        public float Level { get;  set; }
        public long ActiveStatusId { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public long? CreatedBy { get;  set; }
    }
}
