using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Contracts;

public interface IInherentOccurrenceProbabilityValueRepository : IRepository<InherentOccurrenceProbabilityValue>
{
    Task<InherentOccurrenceProbabilityValue> GetById(InherentOccurrenceProbabilityValueId id);
}