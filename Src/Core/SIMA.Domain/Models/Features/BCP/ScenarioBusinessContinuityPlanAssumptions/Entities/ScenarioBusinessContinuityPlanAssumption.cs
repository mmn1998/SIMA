using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Args;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Contracts;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Entities
{
    public class ScenarioBusinessContinuityPlanAssumption : Entity
    {
        private ScenarioBusinessContinuityPlanAssumption()
        {

        }
        private ScenarioBusinessContinuityPlanAssumption(CreateScenarioBusinessContinuityPlanAssumptionArg arg)
        {
            Id = new ScenarioBusinessContinuityPlanAssumptionId(arg.Id);
            ScenarioId = new(arg.ScenarioId);
            BusinessContinuityPlanAssumptionId = new(arg.BusinessContinuityPlanAssumptionId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<ScenarioBusinessContinuityPlanAssumption> Create(CreateScenarioBusinessContinuityPlanAssumptionArg arg, IScenarioBusinessContinuityPlanAssumptionDomainService service)
        {
            //await CreateGuards(arg, service);
            return new ScenarioBusinessContinuityPlanAssumption(arg);
        }
        public async Task Modify(ModifyScenarioBusinessContinuityPlanAssumptionArg arg, IScenarioBusinessContinuityPlanAssumptionDomainService service)
        {
            //await ModifyGuards(arg, service);
            ScenarioId = new(arg.ScenarioId);
            BusinessContinuityPlanAssumptionId = new(arg.BusinessContinuityPlanAssumptionId);
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }
        #region Guards
        //private static async Task CreateGuards(CreateScenarioBusinessContinuityPlanAssumptionArg arg, IScenarioBusinessContinuityPlanAssumptionDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        //private async Task ModifyGuards(ModifyScenarioBusinessContinuityPlanAssumptionArg arg, IScenarioBusinessContinuityPlanAssumptionDomainService service)
        //{
        //    arg.NullCheck();
        //    arg.Name.NullCheck();
        //    arg.Code.NullCheck();

        //    if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        //    if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        //    if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        //}
        #endregion
        public ScenarioBusinessContinuityPlanAssumptionId Id { get; set; }
        public ScenarioId ScenarioId { get; private set; }
        public virtual Scenario Scenario { get; private set; }
        public BusinessContinuityPlanAssumptionId BusinessContinuityPlanAssumptionId { get; private set; }
        public virtual BusinessContinuityPlanAssumption BusinessContinuityPlanAssumption { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
