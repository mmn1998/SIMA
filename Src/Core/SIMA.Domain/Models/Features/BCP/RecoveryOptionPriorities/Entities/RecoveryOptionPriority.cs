using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Args;
using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Entities
{
    public class RecoveryOptionPriority : Entity
    {
        private RecoveryOptionPriority()
        {

        }
        private RecoveryOptionPriority(CreateRecoveryOptionPriorityArg arg)
        {
            Id = new RecoveryOptionPriorityId(arg.Id);
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            Ordering = arg.Ordering;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<RecoveryOptionPriority> Create(CreateRecoveryOptionPriorityArg arg, IRecoveryOptionPriorityDomainService service)
        {
            await CreateGuards(arg, service);
            return new RecoveryOptionPriority(arg);
        }
        public async Task Modify(ModifyRecoveryOptionPriorityArg arg, IRecoveryOptionPriorityDomainService service)
        {
            await ModifyGuards(arg, service);
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            Ordering = arg.Ordering;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }
        #region Guards
        private static async Task CreateGuards(CreateRecoveryOptionPriorityArg arg, IRecoveryOptionPriorityDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        private async Task ModifyGuards(ModifyRecoveryOptionPriorityArg arg, IRecoveryOptionPriorityDomainService service)
        {
            arg.NullCheck();
            arg.Name.NullCheck();
            arg.Code.NullCheck();

            if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        #endregion
        public RecoveryOptionPriorityId Id { get; set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public float Ordering { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
