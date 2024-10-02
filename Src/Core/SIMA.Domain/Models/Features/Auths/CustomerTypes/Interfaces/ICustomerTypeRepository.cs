using SIMA.Domain.Models.Features.Auths.CustomerTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.CustomerTypes.Interfaces;

public interface ICustomerTypeRepository : IRepository<CustomerType>
{
    Task<CustomerType> GetById(CustomerTypeId Id);
}