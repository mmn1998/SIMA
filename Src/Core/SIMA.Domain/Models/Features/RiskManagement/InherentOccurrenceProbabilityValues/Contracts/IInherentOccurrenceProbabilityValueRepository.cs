using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Contracts;

public interface IInherentOccurrenceProbabilityValueRepository : IRepository<InherentOccurrenceProbabilityValue>
{
    Task<InherentOccurrenceProbabilityValue> GetById(InherentOccurrenceProbabilityValueId id);
}