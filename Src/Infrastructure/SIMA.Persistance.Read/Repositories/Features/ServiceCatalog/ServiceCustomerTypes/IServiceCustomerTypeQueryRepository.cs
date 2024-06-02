using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCustomerTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceCustomerTypes;

public interface IServiceCustomerTypeQueryRepository : IQueryRepository
{
    Task<GetServiceCustomerTypeQueryResult> GetById(GetServiceCustomerTypeQuery request);
    Task<Result<IEnumerable<GetServiceCustomerTypeQueryResult>>> GetAll(GetAllServiceCustomerTypesQuery request);
}