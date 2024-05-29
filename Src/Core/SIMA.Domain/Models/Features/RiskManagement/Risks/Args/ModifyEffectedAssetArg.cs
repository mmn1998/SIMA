namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args
{
    public class ModifyEffectedAssetArg
    {
        public long RiskId { get; set; }
        //public long AssetId { get; set; }
        public int ARO { get; set; }
        public double AV { get; set; }
        public float EF { get; set; }
        public float SLE { get; set; }
        public float ALE { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
