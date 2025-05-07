using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Contracts;

public interface ICurrentOccurrenceProbabilityRepository : IRepository<CurrentOccurrenceProbability>
{
    Task<CurrentOccurrenceProbability> GetById(CurrentOccurrenceProbabilityId id);
}