namespace SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Args
{
    public class ModifyRiskPossibilityArgs
    {
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Code { get;  set; }
        public float Possibility { get;  set; }
        public long ActiveStatusId { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
