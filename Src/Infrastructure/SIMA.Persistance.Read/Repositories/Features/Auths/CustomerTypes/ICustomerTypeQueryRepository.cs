using SIMA.Application.Query.Contract.Features.Auths.CustomerTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.CustomerTypes;

public interface ICustomerTypeQueryRepository : IQueryRepository
{
    Task<GetCustomerTypeQueryResult> GetById(GetCustomerTypeQuery request);
    Task<Result<IEnumerable<GetCustomerTypeQueryResult>>> GetAll(GetAllCustomerTypesQuery request);
}