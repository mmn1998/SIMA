using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ConsequenceValues.Contracts;

public interface IConsequenceValueRepository : IRepository<ConsequenceValue>
{
    Task<ConsequenceValue> GetById(ConsequenceValueId id);
}