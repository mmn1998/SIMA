using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServicePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServicePriorities
{
    public interface IServicePriorityQueryRepository : IQueryRepository
    {
        Task<GetAllServicePrioritiesQueryResult> GetById(GetServicePriorityQuery request);
        Task<Result<IEnumerable<GetAllServicePrioritiesQueryResult>>> GetAll(GetAllServicePrioritiesQuery request);
    }
}
