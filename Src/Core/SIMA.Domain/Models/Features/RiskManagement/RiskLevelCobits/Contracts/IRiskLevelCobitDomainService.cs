using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Contracts;

public interface IRiskLevelCobitDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, RiskLevelCobitId? id = null);
    Task<bool> IsNumericUnique(float value, RiskLevelCobitId? id = null);
}