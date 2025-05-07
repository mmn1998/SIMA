using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.AddressTypes.Interfaces;

public interface IAddressTypeRepository : IRepository<AddressType>
{
    Task<AddressType> GetById(long id);
}
