using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Interfaces;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities
{
    public class RiskType : Entity
    {
        private RiskType()
        {
            
        }
        private RiskType(CreateRiskTypeArgs arg)
        {
            Id = new RiskTypeId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RiskType> Create(CreateRiskTypeArgs arg , IRiskTypeDomainService service)
        {
            await CreateGuard(arg, service);
            return new RiskType(arg);
        }
        public async Task Modify(ModifyRiskTypeArgs arg, IRiskTypeDomainService service)
        {
            await ModifyGuard(arg, service);
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

        public RiskTypeId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<Risk> _risks = new();
        public ICollection<Risk> Risks => _risks;

        #region Guards
        private static async Task CreateGuard(CreateRiskTypeArgs arg, IRiskTypeDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }

        private async Task ModifyGuard(ModifyRiskTypeArgs arg, IRiskTypeDomainService service)
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
