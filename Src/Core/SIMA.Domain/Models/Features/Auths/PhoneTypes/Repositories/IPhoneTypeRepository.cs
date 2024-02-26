using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.PhoneTypes.Repositories;

public interface IPhoneTypeRepository : IRepository<PhoneType>
{
    Task<PhoneType> GetById(long id);
}
