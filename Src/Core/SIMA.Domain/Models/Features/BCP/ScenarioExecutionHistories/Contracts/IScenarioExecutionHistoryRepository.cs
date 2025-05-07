using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Contracts
{
    public interface IScenarioExecutionHistoryRepository : IRepository<ScenarioExecutionHistory>
    {
        Task<ScenarioExecutionHistory> GetById(ScenarioExecutionHistoryId id);
    }
}
