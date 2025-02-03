using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceTypes;

public interface IServiceTypeQueryRepository : IQueryRepository
{
    Task<GetServiceTypesQueryResult> GetById(GetServiceTypeQuery request);
    Task<Result<IEnumerable<GetServiceTypesQueryResult>>> GetAll(GetAllServiceTypesQuery request);
}
