using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Contracts
{
    public interface IScenarioBusinessContinuityPlanVersioningRepisitory : IRepository<ScenarioBusinessContinuityPlanVersioning>
    {
        Task<ScenarioBusinessContinuityPlanVersioning> GetById(ScenarioBusinessContinuityPlanVersioningId id);
    }
}
