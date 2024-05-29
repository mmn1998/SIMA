using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibillities.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities
{
    public class RiskLevelMeasure : Entity
    {
        private RiskLevelMeasure()
        {
            
        }
        private RiskLevelMeasure(CreateRiskLevelMeasureArg arg)
        {
            Id = new RiskLevelMeasureId(IdHelper.GenerateUniqueId());
            Code = arg.Code;
            RiskLevelId = new RiskLevelId(arg.RiskLevelId);
            RiskPossibilityId = new RiskPossibilityId(arg.RiskPossibilityId);
            RiskImpactId = new RiskImpactId(arg.RiskImpactId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RiskLevelMeasure> Create(CreateRiskLevelMeasureArg arg)
        {
            return new RiskLevelMeasure(arg);
        }
        public async Task Modify(ModifyRiskLevelMeasureArg arg)
        {
            Code = arg.Code;
            RiskLevelId = new RiskLevelId(arg.RiskLevelId);
            RiskPossibilityId = new RiskPossibilityId(arg.RiskPossibilityId);
            RiskImpactId = new RiskImpactId(arg.RiskImpactId);
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public RiskLevelMeasureId Id { get; set; }
        public string Code { get; private set; }
        public RiskLevelId RiskLevelId { get; private set; }
        public virtual RiskLevel RiskLevel { get; private set; }
        public RiskPossibilityId RiskPossibilityId { get; private set; }
        public virtual RiskPossibility RiskPossibility { get; private set; }
        public RiskImpactId RiskImpactId { get; private set; }
        public virtual RiskImpact RiskImpact { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
