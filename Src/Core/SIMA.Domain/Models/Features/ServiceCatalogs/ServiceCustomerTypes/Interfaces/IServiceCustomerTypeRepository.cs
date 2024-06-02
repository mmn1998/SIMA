using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Interfaces;

public interface IServiceCustomerTypeRepository : IRepository<ServiceCustomerType>
{
    Task<ServiceCustomerType> GetById(ServiceCustomerTypeId Id);
}