using SIMA.Domain.Models.Features.Auths.AccessTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.AccessTypes.Contracts;

public interface IAccessTypeRepository : IRepository<AccessType>
{
    Task<AccessType> GetById(AccessTypeId id);
}
