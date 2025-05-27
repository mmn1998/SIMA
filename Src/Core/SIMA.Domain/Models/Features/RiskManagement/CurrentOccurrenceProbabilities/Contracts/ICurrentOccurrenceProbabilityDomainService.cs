using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Contracts;

public interface ICurrentOccurrenceProbabilityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CurrentOccurrenceProbabilityId? id = null);

    Task<bool> IsRelationsUnique(FrequencyId frequencyId , InherentOccurrenceProbabilityValueId inherentOccurrenceProbabilityValueId , CurrentOccurrenceProbabilityId id = null);
}