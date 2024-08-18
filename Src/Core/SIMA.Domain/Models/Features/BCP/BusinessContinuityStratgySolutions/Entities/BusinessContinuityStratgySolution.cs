using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Entities
{
    public class BusinessContinuityStratgySolution : Entity
    {
        private BusinessContinuityStratgySolution()
        {

        }
        private BusinessContinuityStratgySolution(CreateBusinessContinuityStratgySolutionArg arg)
        {
            Id = new BusinessContinuityStratgySolutionId(arg.Id);
            Title = arg.Title;
            Code = arg.Code;
            BusinessContinuityStratgyId = new (arg.BusinessContinuityStratgyId) ;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<BusinessContinuityStratgySolution> Create(CreateBusinessContinuityStratgySolutionArg arg, IBusinessContinuityStratgySolutionDomainService service)
        {
            await CreateGuards(arg, service);
            return new BusinessContinuityStratgySolution(arg);
        }
        public async Task Modify(ModifyBusinessContinuityStratgySolutionArg arg, IBusinessContinuityStratgySolutionDomainService service)
        {
            await ModifyGuards(arg, service);
            Title = arg.Title;
            Code = arg.Code;
            BusinessContinuityStratgyId = new(arg.BusinessContinuityStratgyId);
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }
        #region Guards
        private static async Task CreateGuards(CreateBusinessContinuityStratgySolutionArg arg, IBusinessContinuityStratgySolutionDomainService service)
        {
            arg.NullCheck();
            arg.Title.NullCheck();
            arg.Code.NullCheck();

            if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        private async Task ModifyGuards(ModifyBusinessContinuityStratgySolutionArg arg, IBusinessContinuityStratgySolutionDomainService service)
        {
            arg.NullCheck();
            arg.Title.NullCheck();
            arg.Code.NullCheck();

            if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        #endregion
        public BusinessContinuityStratgySolutionId Id { get; set; }
        public string? Title { get; private set; }
        public string? Code { get; private set; }
        public BusinessContinuityStrategyId BusinessContinuityStratgyId { get; private set; }
        public virtual BusinessContinuityStrategy BusinessContinuityStratgy { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
