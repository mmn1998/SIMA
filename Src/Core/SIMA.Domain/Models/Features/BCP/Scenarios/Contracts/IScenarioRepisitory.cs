using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Contracts
{
    public interface IScenarioRepisitory : IRepository<Scenario>
    {
        Task<Scenario> GetById(ScenarioId id);
    }
}
