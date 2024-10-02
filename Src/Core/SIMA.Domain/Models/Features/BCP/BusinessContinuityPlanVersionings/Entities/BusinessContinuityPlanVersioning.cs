using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Contracts;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities
{
    public class BusinessContinuityPlanVersioning : Entity
    {
        private BusinessContinuityPlanVersioning()
        {

        }
        private BusinessContinuityPlanVersioning(CreateBusinessContinuityPlanVersioningArg arg)
        {
            Id = new BusinessContinuityPlanVersioningId(arg.Id);
            BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
            VersionNumber = arg.VersionNumber;
            ReleaseDate = arg.ReleaseDate;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<BusinessContinuityPlanVersioning> Create(CreateBusinessContinuityPlanVersioningArg arg, IBusinessContinuityPlanVersioningDomainService service)
        {
            //await CreateGuards(arg, service);
            return new BusinessContinuityPlanVersioning(arg);
        }
        public async Task Modify(ModifyBusinessContinuityPlanVersioningArg arg, IBusinessContinuityPlanVersioningDomainService service)
        {
            //await ModifyGuards(arg, service);
            BusinessContinuityPlanId = new (arg.BusinessContinuityPlanId);
            VersionNumber = arg.VersionNumber;
            ReleaseDate = arg.ReleaseDate;
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }
        #region Guards
        //private static async Task CreateGuards(CreateBusinessContinuityPlanVersioningArg arg, IBusinessContinuityPlanVersioningDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        //private async Task ModifyGuards(ModifyBusinessContinuityPlanVersioningArg arg, IBusinessContinuityPlanVersioningDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        #endregion
        public BusinessContinuityPlanVersioningId Id { get; set; }
        public BusinessContinuityPlanId BusinessContinuityPlanId { get; private set; }
        public virtual BusinessContinuityPlan BusinessContinuityPlan { get; private set; }
        public string VersionNumber { get;  set; }
        public DateTime ReleaseDate { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<BusinessContinuityPlanResponsible> _businessContinuityPlanResponsible = new();
        public ICollection<BusinessContinuityPlanResponsible> BusinessContinuityPlanResponsibles => _businessContinuityPlanResponsible;

        private List<ScenarioBusinessContinuityPlanVersioning> _scenarioBusinessContinuityPlanVersioning = new();
        public ICollection<ScenarioBusinessContinuityPlanVersioning> ScenarioBusinessContinuityPlanVersionings => _scenarioBusinessContinuityPlanVersioning;

        private List<BusinessContinuityPlanAssumption> _businessContinuityPlanAssumption = new();
        public ICollection<BusinessContinuityPlanAssumption> BusinessContinuityPlanAssumptions => _businessContinuityPlanAssumption;

        private List<BusinessContinuityPlanRisk> _businessContinuityPlanRisk = new();
        public ICollection<BusinessContinuityPlanRisk> BusinessContinuityPlanRisks => _businessContinuityPlanRisk;

        private List<BusinessContinuityPlanService> _businessContinuityPlanServices = new();
        public ICollection<BusinessContinuityPlanService> BusinessContinuityPlanServices => _businessContinuityPlanServices;

        private List<BusinessContinuityPlanStratgy> _businessContinuityPlanStratgy = new();
        public ICollection<BusinessContinuityPlanStratgy> BusinessContinuityPlanStratgies => _businessContinuityPlanStratgy;

        private List<BusinessContinuityPlanCriticalActivity> _businessContinuityPlanCriticalActivities = new();
        public ICollection<BusinessContinuityPlanCriticalActivity> BusinessContinuityPlanCriticalActivities => _businessContinuityPlanCriticalActivities;

        private List<BusinessContinuityPlanRelatedStaff> _BusinessContinuityPlanRelatedStaff = new();
        public ICollection<BusinessContinuityPlanRelatedStaff> BusinessContinuityPlanRelatedStaff => _BusinessContinuityPlanRelatedStaff;


    }
}
