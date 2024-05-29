using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Vulnerabilities.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities
{
    public class EffectedAsset : Entity
    {
        private EffectedAsset()
        {
            
        }
        private EffectedAsset(CreateEffectedAssetArgs arg)
        {
            Id = new EffectedAssetId(IdHelper.GenerateUniqueId());
            RiskId = new RiskId(arg.RiskId);
            ARO = arg.ARO;
            AV = arg.AV;
            EF = arg.EF;
            SLE = arg.SLE;
            ALE = arg.ALE;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<EffectedAsset> Create(CreateEffectedAssetArgs arg)
        {
            return new EffectedAsset(arg);
        }
        public async Task Modify(ModifyEffectedAssetArg arg)
        {
            RiskId = new RiskId(arg.RiskId);
            ARO = arg.ARO;
            AV = arg.AV;
            EF = arg.EF;
            SLE = arg.SLE;
            ALE = arg.ALE;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }

        public EffectedAssetId Id { get; private set; }
        public RiskId RiskId { get; private set; }
        public virtual Risk Risk { get; private set; }
        // public AssetId AssetId { get; private set; }
        //public virtual Asset Asset { get; private set; }
        public int ARO { get; private set; }
        public double AV { get; private set; }
        public float EF { get; private set; }
        public float SLE { get; private set; }
        public float ALE { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<Vulnerability> _vulnerabilities = new();
        public ICollection<Vulnerability> Vulnerabilities => _vulnerabilities;

    }
}
