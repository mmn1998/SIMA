using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryOptions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ScenarioRecoveryOptions.Contracts
{
    public interface IScenarioRecoveryOptionRepisitory : IRepository<ScenarioRecoveryOption>
    {
        Task<ScenarioRecoveryOption> GetById(ScenarioRecoveryOptionId id);
    }
}
