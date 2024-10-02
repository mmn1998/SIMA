using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Contracts;

public interface IScenarioBusinessContinuityPlanAssumptionDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ScenarioBusinessContinuityPlanAssumptionId? id = null);
}
