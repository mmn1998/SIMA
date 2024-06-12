using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities
{
    public class RiskImpact : Entity
    {
        private RiskImpact()
        {
            
        }
        private RiskImpact(CreateRiskImpactArgs arg)
        {
            Id = new RiskImpactId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            Impact = arg.Impact;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RiskImpact> Create(CreateRiskImpactArgs arg, IRiskImpactDomainService service)
        {
            await CreateGuard(arg, service);
            return new RiskImpact(arg);
        }
        public async Task Modify(ModifyRiskImpactArgs arg, IRiskImpactDomainService service)
        {
            await ModifyGuard(arg, service);
            Name = arg.Name;
            Code = arg.Code;
            Impact = arg.Impact;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public RiskImpactId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public float Impact { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<RiskCriteria> _riskCriterias = new();
        public ICollection<RiskCriteria> RiskCriterias => _riskCriterias;

        private List<RiskLevelMeasure> _riskLevelMeasures = new();
        public ICollection<RiskLevelMeasure> RiskLevelMeasures => _riskLevelMeasures;

        private List<ServiceRiskImpact> _serviceRiskImpacts = new();
        public ICollection<ServiceRiskImpact> ServiceRiskImpacts => _serviceRiskImpacts;

        #region Guards
        private static async Task CreateGuard(CreateRiskImpactArgs arg, IRiskImpactDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }

        private async Task ModifyGuard(ModifyRiskImpactArgs arg, IRiskImpactDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        #endregion


    }
}
