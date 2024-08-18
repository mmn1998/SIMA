using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.ScenarioRecoveryOptions.Contracts
{
    public interface IScenarioRecoveryOptionDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, ScenarioRecoveryOptionId? id = null);
    }
}
