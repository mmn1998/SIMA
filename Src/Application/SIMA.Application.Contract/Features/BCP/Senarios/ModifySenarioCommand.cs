using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.Senarios
{
    public class ModifySenarioCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<CreateScenarioBusinessContinuityPlanAssumptionCommand>? ScenarioBusinessContinuityPlanAssumptions { get; set; }
        public List<CreateScenarioBusinessContinuityPlanVersioningCommand>? ScenarioBusinessContinuityPlanVersionings { get; set; }
        public List<CreateScenarioPlanRecoveryCriteriaCommand>? ScenarioPlanRecoveryCriterias { get; set; }
        public List<CreateScenarioPossibleActionCommand>? ScenarioPossibleActions { get; set; }
        public List<CreateScenarioRecoveryOptionCommand>? ScenarioRecoveryOptions { get; set; }
    }
}
