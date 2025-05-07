using SIMA.Domain.Models.Features.BCP.BiaValues.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BiaValues.Contracts;

public interface IBiaValueRepository : IRepository<BiaValue>
{
    Task<BiaValue> GetById(BiaValueId id);
}