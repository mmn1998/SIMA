using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BCP.Senarios
{
    public class CreateSenarioCommand : ICommand<Result<long>>
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public List<CreateScenarioBusinessContinuityPlanAssumptionCommand>? ScenarioBusinessContinuityPlanAssumptions { get; set; }
        public List<CreateScenarioBusinessContinuityPlanVersioningCommand>? ScenarioBusinessContinuityPlanVersionings { get; set; }
        public List<CreateScenarioPlanRecoveryCriteriaCommand>? ScenarioPlanRecoveryCriterias { get; set; }
        public List<CreateScenarioPossibleActionCommand>? ScenarioPossibleActions { get; set; }
        public List<CreateScenarioRecoveryOptionCommand>? ScenarioRecoveryOptions { get; set; }

    }
}
