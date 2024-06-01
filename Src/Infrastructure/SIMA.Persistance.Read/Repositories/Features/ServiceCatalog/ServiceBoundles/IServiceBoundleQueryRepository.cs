using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceBoundles;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceBoundles;

public interface IServiceBoundleQueryRepository : IQueryRepository
{
    Task<GetServiceBoundleQueryResult> GetById(GetServiceBoundleQuery request);
    Task<Result<IEnumerable<GetServiceBoundleQueryResult>>> GetAll(GetAllServiceBoundlesQuery request);
}