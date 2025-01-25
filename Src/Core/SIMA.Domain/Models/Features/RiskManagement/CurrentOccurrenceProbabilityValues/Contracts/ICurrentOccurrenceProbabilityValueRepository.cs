using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Contracts;

public interface ICurrentOccurrenceProbabilityValueRepository : IRepository<CurrentOccurrenceProbabilityValue>
{
    Task<CurrentOccurrenceProbabilityValue> GetById(CurrentOccurrenceProbabilityValueId id);
}
