using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Interfaces;

public interface IServiceTypeRepository : IRepository<ServiceType>
{
    Task<ServiceType> GetById(ServiceTypeId Id);
}