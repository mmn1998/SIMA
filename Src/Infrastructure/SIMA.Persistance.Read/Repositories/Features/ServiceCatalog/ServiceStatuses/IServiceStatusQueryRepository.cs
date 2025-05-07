using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceStatuses
{
    public interface IServiceStatusQueryRepository : IQueryRepository
    {
        Task<GetServiceStatusesQueryResult> GetById(GetServiceStatusQuery request);
        Task<Result<IEnumerable<GetServiceStatusesQueryResult>>> GetAll(GetAllServiceStatusesQuery request);
    }
}
