using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Args;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Contracts;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Args;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Contracts;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Entities
{
    public class ScenarioBusinessContinuityPlanVersioning : Entity
    {
        private ScenarioBusinessContinuityPlanVersioning()
        {

        }
        private ScenarioBusinessContinuityPlanVersioning(CreateScenarioBusinessContinuityPlanVersioningArg arg)
        {
            Id = new ScenarioBusinessContinuityPlanVersioningId(arg.Id);
            BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
            ScenarioId = new(arg.ScenarioId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<ScenarioBusinessContinuityPlanVersioning> Create(CreateScenarioBusinessContinuityPlanVersioningArg arg, IScenarioBusinessContinuityPlanVersioningDomainService service)
        {
            //await CreateGuards(arg, service);
            return new ScenarioBusinessContinuityPlanVersioning(arg);
        }
        public async Task Modify(ModifyScenarioBusinessContinuityPlanVersioningArg arg, IScenarioBusinessContinuityPlanVersioningDomainService service)
        {
            //await ModifyGuards(arg, service);
            BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
            ScenarioId = new(arg.ScenarioId);
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }
        #region Guards
        //private static async Task CreateGuards(CreateScenarioBusinessContinuityPlanVersioningArg arg, IScenarioBusinessContinuityPlanVersioningDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        //private async Task ModifyGuards(ModifyScenarioBusinessContinuityPlanVersioningArg arg, IScenarioBusinessContinuityPlanVersioningDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        #endregion
        public ScenarioBusinessContinuityPlanVersioningId Id { get; set; }
        public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; set; }
        public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; set; }
        public ScenarioId ScenarioId { get; private set; }
        public Scenario Scenario { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
