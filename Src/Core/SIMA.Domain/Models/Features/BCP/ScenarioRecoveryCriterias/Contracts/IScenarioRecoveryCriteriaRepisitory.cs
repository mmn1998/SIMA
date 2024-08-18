using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Contracts
{
    public interface IScenarioRecoveryCriteriaRepisitory : IRepository<ScenarioRecoveryCriteria>
    {
        Task<ScenarioRecoveryCriteria> GetById(ScenarioRecoveryCriteriaId id);
    }
}
