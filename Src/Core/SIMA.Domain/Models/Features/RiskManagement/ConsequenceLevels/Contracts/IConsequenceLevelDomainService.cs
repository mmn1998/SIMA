using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Contracts;

public interface IConsequenceLevelDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ConsequenceLevelId? id = null);
    Task<bool> IsNumericUnique(float value, ConsequenceLevelId? id = null);
    Task CanBeDeleted(ConsequenceLevelId id);
}