using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.BCP.Scenarios;

public class GetScenarioQueryResult 
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public List<GetScenarioBusinessContinuityPlanVersioning>? ScenarioBusinessContinuityPlanVersioningList { get; set; }
    public List<GetScenarioBusinessContinuityPlanAssumption>? ScenarioBusinessContinuityPlanAssumptionList { get; set; }
    public List<GetScenarioPlanRecoveryCriteria>? ScenarioPlanRecoveryCriteriaList { get; set; }
    public List<GetScenarioPossibleAction>? ScenarioPossibleActionList { get; set; }
    public List<GetScenarioRecoveryOption>? ScenarioRecoveryOptionList { get; set; }

}
