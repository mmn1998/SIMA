using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.StepApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Entities
{
    public class ApprovalOption : Entity
    {
        private ApprovalOption()
        {
        }

        public ApprovalOption(CreateApprovalOptionArg arg)
        {
            Id = new ApprovalOptionId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<ApprovalOption> Create(CreateApprovalOptionArg arg, IApprovalOptionDomainService service)
        {
            await CreateGuard(arg, service);
            return new ApprovalOption(arg);
        }

        public async Task Modify(ModifyApprovalOptionArg arg, IApprovalOptionDomainService service)
        {
            await ModifyGuard(arg, service);
            Code = arg.Code;
            Name = arg.Name;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
        }
        public void Delete()
        {
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public ApprovalOptionId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public long? ModifiedBy { get; private set; }
        public byte[] ModifiedAt { get; private set; }

        private List<StepApprovalOption> _stepApprovalOptions = new();
        public ICollection<StepApprovalOption> StepApprovalOptions => _stepApprovalOptions;


        #region Guards
        private static async Task CreateGuard(CreateApprovalOptionArg arg, IApprovalOptionDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }

        private async Task ModifyGuard(ModifyApprovalOptionArg arg, IApprovalOptionDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (await service.IsCodeUnique(arg.Code, Id.Value)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        #endregion

    }
}
