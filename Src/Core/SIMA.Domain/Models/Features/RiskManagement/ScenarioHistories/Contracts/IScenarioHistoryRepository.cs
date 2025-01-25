using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Contracts;

public interface IScenarioHistoryRepository : IRepository<ScenarioHistory>
{
    Task<ScenarioHistory> GetById(ScenarioHistoryId id);
}