using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Contracts;

public interface ICurrentOccurrenceProbabilityValueDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CurrentOccurrenceProbabilityValueId? id = null);
    Task<bool> IsNumericUnique(float value, CurrentOccurrenceProbabilityValueId? id = null);
}