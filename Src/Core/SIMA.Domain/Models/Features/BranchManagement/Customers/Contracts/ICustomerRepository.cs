using SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Customers.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.Customers.Contracts;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> GetById(CustomerId id);
    Task<Customer> GetByCode(string code);
}