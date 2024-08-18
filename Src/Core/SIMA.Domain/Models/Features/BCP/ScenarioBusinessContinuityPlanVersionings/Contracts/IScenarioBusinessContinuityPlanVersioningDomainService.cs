using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Contracts
{
    public interface IScenarioBusinessContinuityPlanVersioningDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, ScenarioBusinessContinuityPlanVersioningId? id = null);
    }
}
