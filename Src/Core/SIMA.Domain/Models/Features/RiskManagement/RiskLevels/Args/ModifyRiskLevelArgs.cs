namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Args
{
    public class ModifyRiskLevelArgs
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float Level { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
