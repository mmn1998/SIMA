using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Contracts;

public interface IScenarioHistoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ScenarioHistoryId? id = null);
}