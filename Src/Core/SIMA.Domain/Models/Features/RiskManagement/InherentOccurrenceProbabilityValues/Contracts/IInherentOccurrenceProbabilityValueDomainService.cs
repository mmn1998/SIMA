using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Contracts;

public interface IInherentOccurrenceProbabilityValueDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, InherentOccurrenceProbabilityValueId? id = null);
    Task<bool> IsNumericUnique(float value, InherentOccurrenceProbabilityValueId? id = null);
}