using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Args;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Entities
{
    public class ImpactScale : Entity
    {
        private ImpactScale()
        {
            
        }
        private ImpactScale(CreateImpactScaleArg arg)
        {
            Id = new ImpactScaleId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<ImpactScale> Create(CreateImpactScaleArg arg)
        {
            return new ImpactScale(arg);
        }
        public async Task Modify(ModifyImpactScaleArg arg)
        {
            Name = arg.Name;
            Code = arg.Code;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public ImpactScaleId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<ServiceRiskImpact> _serviceRiskImpacts = new();
        public ICollection<ServiceRiskImpact> ServiceRiskImpacts => _serviceRiskImpacts;

    }
}
