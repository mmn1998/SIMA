using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Interfaces;

public interface IServiceUserTypeRepository : IRepository<ServiceUserType>
{
    Task<ServiceUserType> GetById(ServiceUserTypeId Id);
}