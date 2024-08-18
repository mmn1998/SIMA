using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Contracts
{
    public interface IScenarioRecoveryCriteriaDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, ScenarioRecoveryCriteriaId? id = null);
    }
}
