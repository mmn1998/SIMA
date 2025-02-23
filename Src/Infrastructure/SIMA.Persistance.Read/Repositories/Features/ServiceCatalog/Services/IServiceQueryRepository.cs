using SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Services;

public interface IServiceQueryRepository : IQueryRepository
{
    Task<GetServiceQueryResult> GetById(long id, long issueId);
    Task<Result<IEnumerable<GetAllServicesQueryResult>>> GetAll(GetAllServicesQuery request);
    Task<string> GetLastCode();
}