using SIMA.Domain.Models.Features.Auths.AddressTypes.Args;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities
{
    public class RiskDegree : Entity
    {
        private RiskDegree()
        {
            
        }
        private RiskDegree(CreateRiskDegreeArgs arg)
        {
            Id = new RiskDegreeId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            Degree = arg.Degree;
            Color = arg.Color;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RiskDegree> Create(CreateRiskDegreeArgs arg)
        {
            return new RiskDegree(arg);
        }
        public async Task Modify(ModifyRiskDegreeArgs arg)
        {
            Name = arg.Name;
            Code = arg.Code;
            Degree = arg.Degree;
            Color = arg.Color;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }

        public RiskDegreeId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public float Degree { get; private set; }
        public string Color { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<RiskCriteria> _riskCriterias = new();
        public ICollection<RiskCriteria> RiskCriterias => _riskCriterias;
    }
}
