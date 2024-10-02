using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryOptions.Entities;
using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Domain.Models.Features.BCP.Scenarios.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Entities
{
    public class Scenario : Entity
    {
        private Scenario()
        {

        }
        private Scenario(CreateScenarioArg arg)
        {
            Id = new ScenarioId(arg.Id);
            Title = arg.Title;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<Scenario> Create(CreateScenarioArg arg, IScenarioDomainService service)
        {
            await CreateGuards(arg, service);
            return new Scenario(arg);
        }
        public async Task Modify(ModifyScenarioArg arg, IScenarioDomainService service)
        {
            await ModifyGuards(arg, service);
            Title = arg.Title;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }

        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        #region Guards
        private static async Task CreateGuards(CreateScenarioArg arg, IScenarioDomainService service)
        {
            arg.NullCheck();
            arg.Title.NullCheck();
            arg.Code.NullCheck();

            if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        private async Task ModifyGuards(ModifyScenarioArg arg, IScenarioDomainService service)
        {
            arg.NullCheck();
            arg.Title.NullCheck();
            arg.Code.NullCheck();

            if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        #endregion
        public ScenarioId Id { get; set; }
        public string Title { get; private set; }
        public string Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<ScenarioRecoveryOption> _scenarioRecoveryOption = new();
        public ICollection<ScenarioRecoveryOption> ScenarioRecoveryOptions => _scenarioRecoveryOption;

        private List<ScenarioRecoveryCriteria> _scenarioRecoveryCriteria = new();
        public ICollection<ScenarioRecoveryCriteria> ScenarioRecoveryCriterias => _scenarioRecoveryCriteria;

        private List<ScenarioBusinessContinuityPlanAssumption> _scenarioBusinessContinuityPlanAssumption = new();
        public ICollection<ScenarioBusinessContinuityPlanAssumption> ScenarioBusinessContinuityPlanAssumptions => _scenarioBusinessContinuityPlanAssumption;

        private List<ScenarioBusinessContinuityPlanVersioning> _scenarioBusinessContinuityPlanVersioning = new();
        public ICollection<ScenarioBusinessContinuityPlanVersioning> ScenarioBusinessContinuityPlanVersionings => _scenarioBusinessContinuityPlanVersioning;


        
    }
}
