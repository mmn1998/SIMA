using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Contracts;

public interface IInherentOccurrenceProbabilityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, InherentOccurrenceProbabilityId? id = null);
}