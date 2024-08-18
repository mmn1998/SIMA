using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Contracts;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities
{
    public class BusinessContinuityStratgyResponsible : Entity
    {
        private BusinessContinuityStratgyResponsible()
        {

        }
        private BusinessContinuityStratgyResponsible(CreateBusinessContinuityStratgyResponsibleArg arg)
        {
            Id = new BusinessContinuityStratgyResponsibleId(arg.Id);
            BusinessContinuityStrategyId = new(arg.BusinessContinuityStrategyId);
            StaffId = new(arg.StaffId);
            PlanResponsibilityId = new(arg.PlanResponsibilityId);
            IsForBackup = arg.IsForBackup;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<BusinessContinuityStratgyResponsible> Create(CreateBusinessContinuityStratgyResponsibleArg arg, IBusinessContinuityStratgyResponsibleDomainService service)
        {
            //await CreateGuards(arg, service);
            return new BusinessContinuityStratgyResponsible(arg);
        }
        public async Task Modify(ModifyBusinessContinuityStratgyResponsibleArg arg, IBusinessContinuityStratgyResponsibleDomainService service)
        {
            //await ModifyGuards(arg, service);
            BusinessContinuityStrategyId = new(arg.BusinessContinuityStrategyId);
            StaffId = new (arg.StaffId);
            PlanResponsibilityId = new (arg.PlanResponsibilityId);
            IsForBackup = arg.IsForBackup;
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }
        #region Guards
        //private static async Task CreateGuards(CreateBusinessContinuityStratgyResponsibleArg arg, IBusinessContinuityStratgyResponsibleDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        //private async Task ModifyGuards(ModifyBusinessContinuityStratgyResponsibleArg arg, IBusinessContinuityStratgyResponsibleDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        #endregion
        public BusinessContinuityStratgyResponsibleId Id { get; set; }
        public BusinessContinuityStrategyId BusinessContinuityStrategyId { get; private set; }
        public virtual BusinessContinuityStrategy BusinessContinuityStrategy { get; private set; }
        public StaffId StaffId { get; private set; }
        public virtual Staff Staff { get; private set; }
        public PlanResponsibilityId PlanResponsibilityId { get; private set; }
        public virtual PlanResponsibility PlanResponsibility { get; private set; }
        public string IsForBackup { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
