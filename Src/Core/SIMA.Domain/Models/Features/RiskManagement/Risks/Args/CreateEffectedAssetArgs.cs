namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args
{
    public class CreateEffectedAssetArgs
    {
        public long RiskId { get; set; }
        //public long AssetId { get; set; }
        public int ARO { get; set; }
        public double AV { get; set; }
        public float EF { get; set; }
        public float SLE { get; set; }
        public float ALE { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
