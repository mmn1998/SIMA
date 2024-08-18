using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Contracts
{
    public interface IScenarioDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, ScenarioId? id = null);
    }
}
