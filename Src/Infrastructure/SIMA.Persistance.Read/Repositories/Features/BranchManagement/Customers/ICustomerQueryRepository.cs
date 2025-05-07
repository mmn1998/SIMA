using SIMA.Application.Query.Contract.Features.BranchManagement.Customers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.Customers;

public interface ICustomerQueryRepository : IQueryRepository
{
    Task<GetCustomerQueryResult> GetById(GetCustomerQuery request);
    Task<Result<IEnumerable<GetCustomerQueryResult>>> GetAll(GetAllCustomersQuery request);
}