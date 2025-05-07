using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Contracts;

public interface IScenarioHistoryRepository : IRepository<ScenarioHistory>
{
    Task<ScenarioHistory> GetById(ScenarioHistoryId id);
}