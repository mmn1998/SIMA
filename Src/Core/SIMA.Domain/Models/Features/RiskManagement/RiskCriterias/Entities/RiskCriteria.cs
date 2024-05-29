using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibillities.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities
{
    public class RiskCriteria : Entity
    {
        private RiskCriteria()
        {
            
        }
        private RiskCriteria(CreateRiskCriteriaArg arg)
        {
            Id = new RiskCriteriaId(IdHelper.GenerateUniqueId());
            Code = arg.Code;
            RiskDegreeId = new RiskDegreeId(arg.RiskDegreeId);
            RiskPossibilityId = new RiskPossibilityId(arg.RiskPossibilityId);
            RiskImpactId = new RiskImpactId(arg.RiskImpactId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RiskCriteria> Create(CreateRiskCriteriaArg arg)
        {
            return new RiskCriteria(arg);
        }
        public async Task Modify(ModifyRiskCriteriaArg arg)
        {
            Code = arg.Code;
            RiskDegreeId = new RiskDegreeId(arg.RiskDegreeId);
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
        public RiskCriteriaId Id { get; set; }
        public string Code { get; private set; }
        public RiskDegreeId RiskDegreeId { get; private set; }
        public virtual RiskDegree RiskDegree { get; private set; }
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
