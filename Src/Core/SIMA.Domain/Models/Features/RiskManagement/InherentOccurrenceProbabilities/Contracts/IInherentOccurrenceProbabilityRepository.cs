using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Contracts;

public interface IInherentOccurrenceProbabilityRepository : IRepository<InherentOccurrenceProbability>
{
    Task<InherentOccurrenceProbability> GetById(InherentOccurrenceProbabilityId id);
}