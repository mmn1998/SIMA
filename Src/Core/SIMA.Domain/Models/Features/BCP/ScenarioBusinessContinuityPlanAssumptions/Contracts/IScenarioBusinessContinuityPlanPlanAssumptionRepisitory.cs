using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Contracts
{
    public interface IScenarioBusinessContinuityPlanAssumptionRepisitory : IRepository<ScenarioBusinessContinuityPlanAssumption>
    {
        Task<ScenarioBusinessContinuityPlanAssumption> GetById(ScenarioBusinessContinuityPlanAssumptionId id);
    }
}
