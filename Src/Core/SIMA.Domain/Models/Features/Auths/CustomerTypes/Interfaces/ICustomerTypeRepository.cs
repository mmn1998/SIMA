using SIMA.Domain.Models.Features.Auths.ServiceCustomerTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.ServiceCustomerTypes.Interfaces;

public interface ICustomerTypeRepository : IRepository<CustomerType>
{
    Task<CustomerType> GetById(CustomerTypeId Id);
}