using Org.BouncyCastle.Crypto;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Args;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskPossibillities.Entities
{
    public class RiskPossibility : Entity
    {
        private RiskPossibility()
        {
            
        }
        private RiskPossibility(CreateRiskPossibilityArgs arg)
        {
            Id = new RiskPossibilityId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            Possibility = arg.Possibility;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RiskPossibility> Create(CreateRiskPossibilityArgs arg)
        {
            return new RiskPossibility(arg);
        }
        public async Task Modify(ModifyRiskPossibilityArgs arg)
        {
            Name = arg.Name;
            Code = arg.Code;
            Possibility = arg.Possibility;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public RiskPossibilityId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public float Possibility { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<RiskCriteria> _riskCriterias = new();
        public ICollection<RiskCriteria> RiskCriterias => _riskCriterias;

        private List<RiskLevelMeasure> _riskLevelMeasures = new();
        public ICollection<RiskLevelMeasure> RiskLevelMeasures => _riskLevelMeasures;

        private List<Threat> _threats = new();
        public ICollection<Threat> Threats => _threats;

    }
}
