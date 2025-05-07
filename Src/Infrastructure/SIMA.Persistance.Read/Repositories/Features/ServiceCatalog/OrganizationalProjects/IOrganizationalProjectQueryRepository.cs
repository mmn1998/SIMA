using SIMA.Application.Query.Contract.Features.ServiceCatalog.OrganizationalProjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.OrganizationalProjects;

public interface IOrganizationalProjectQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetOrganizationalProjectsQueryResult>>> GetAll(GetAllOrganizationalProjectsQuery request);
    Task<GetOrganizationalProjectsQueryResult> GetById(GetOrganizationalProjectQuery request);
}
