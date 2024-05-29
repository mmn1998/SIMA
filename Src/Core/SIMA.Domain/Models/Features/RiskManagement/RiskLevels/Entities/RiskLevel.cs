using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities
{
    public class RiskLevel : Entity
    {
        private RiskLevel()
        {
            
        }
        private RiskLevel(CreateRiskLevelArgs arg)
        {
            Id = new RiskLevelId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            Level = arg.Level;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RiskLevel> Create(CreateRiskLevelArgs arg)
        {
            return new RiskLevel(arg);
        }
        public async Task Modify(ModifyRiskLevelArgs arg)
        {
            Name = arg.Name;
            Code = arg.Code;
            Level = arg.Level;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public RiskLevelId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public float Level { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<RiskLevelMeasure> _riskLevelMeasures = new();
        public ICollection<RiskLevelMeasure> RiskLevelMeasures => _riskLevelMeasures;

    }
}
