using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Contracts;

public interface IServiceTypeRepository : IRepository<ServiceType>
{
    Task<ServiceType> GetById(ServiceTypeId id);
}
