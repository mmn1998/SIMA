using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities
{
    public class BusinessContinuityPlanStratgy : Entity
    {
        private BusinessContinuityPlanStratgy()
        {

        }
        private BusinessContinuityPlanStratgy(CreateBusinessContinuityPlanStratgyArg arg)
        {
            Id = new BusinessContinuityPlanStratgyId(arg.Id);
            BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
            BusinessContinuityStratgyId = new(arg.BusinessContinuityStratgyId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<BusinessContinuityPlanStratgy> Create(CreateBusinessContinuityPlanStratgyArg arg, IBusinessContinuityPlanStratgyDomainService service)
        {
            //await CreateGuards(arg, service);
            return new BusinessContinuityPlanStratgy(arg);
        }
        public async Task Modify(ModifyBusinessContinuityPlanStratgyArg arg, IBusinessContinuityPlanStratgyDomainService service)
        {
            //await ModifyGuards(arg, service);
            BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
            BusinessContinuityStratgyId = new(arg.BusinessContinuityStratgyId);
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }
        #region Guards
        //private static async Task CreateGuards(CreateBusinessContinuityPlanStratgyArg arg, IBusinessContinuityPlanStratgyDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        //private async Task ModifyGuards(ModifyBusinessContinuityPlanStratgyArg arg, IBusinessContinuityPlanStratgyDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        #endregion
        public BusinessContinuityPlanStratgyId Id { get; set; }
        public BusinessContinuityStrategyId BusinessContinuityStratgyId { get; private set; }
        public virtual BusinessContinuityStrategy BusinessContinuityStrategy { get; private set; }
        public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; private set; }
        public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
