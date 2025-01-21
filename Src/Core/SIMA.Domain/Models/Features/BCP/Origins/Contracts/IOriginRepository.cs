using SIMA.Domain.Models.Features.BCP.Origins.Entities;
using SIMA.Domain.Models.Features.BCP.Origins.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.Origins.Contracts;

public interface IOriginRepository : IRepository<Origin>
{
    Task<Origin> GetById(OriginId id);
}
