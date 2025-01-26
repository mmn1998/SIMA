using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Contracts;

public interface IAffectedHistoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, AffectedHistoryId? id = null);
    Task<bool> IsNumericUnique(float value, AffectedHistoryId? id = null);
}