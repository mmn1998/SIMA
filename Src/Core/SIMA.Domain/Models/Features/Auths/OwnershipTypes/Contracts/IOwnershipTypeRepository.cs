using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.OwnershipTypes.Contracts;

public interface IOwnershipTypeRepository : IRepository<OwnershipType>
{
    Task<OwnershipType> GetById(OwnershipTypeId id);
}
