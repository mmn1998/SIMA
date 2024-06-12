using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

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
            IsImportantBia = arg.IsImportantBia;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RiskDegree> Create(CreateRiskDegreeArgs arg , IRiskDegreeDomainService service)
        {
            await CreateGuard(arg , service);
            return new RiskDegree(arg);
        }
        public async Task Modify(ModifyRiskDegreeArgs arg, IRiskDegreeDomainService service)
        {
            await ModifyGuard(arg, service);
            Name = arg.Name;
            Code = arg.Code;
            Degree = arg.Degree;
            Color = arg.Color;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            IsImportantBia = arg.IsImportantBia;
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
        public string IsImportantBia { get; set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<RiskCriteria> _riskCriterias = new();
        public ICollection<RiskCriteria> RiskCriterias => _riskCriterias;

        #region Guards
        private static async Task CreateGuard(CreateRiskDegreeArgs arg, IRiskDegreeDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (!string.IsNullOrEmpty(arg.Color))
                if (!service.IsHexCodeValid(arg.Color)) throw new SimaResultException("10039", Messages.ColorHexCodeIsInorrectError);

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }

        private async Task ModifyGuard(ModifyRiskDegreeArgs arg, IRiskDegreeDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (!string.IsNullOrEmpty(arg.Color))
                if (!service.IsHexCodeValid(arg.Color)) throw new SimaResultException("10039", Messages.ColorHexCodeIsInorrectError);

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        #endregion
    }
}
