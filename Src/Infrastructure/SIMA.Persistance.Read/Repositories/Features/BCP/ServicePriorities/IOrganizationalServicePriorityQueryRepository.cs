using SIMA.Application.Query.Contract.Features.BCP.ServicePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ServicePriorities;

public interface IOrganizationalServicePriorityQueryRepository : IQueryRepository
{
    Task<GetOrganizationalServicePriorityQueryResult> GetById(GetOrganizationalServicePriorityQuery request);
    Task<Result<IEnumerable<GetOrganizationalServicePriorityQueryResult>>> GetAll(GetAllOrganizationalServicePrioritiesQuery request);
}