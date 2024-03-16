using SIMA.Application.Query.Contract.Features.Auths.Permission;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Permissions;

public interface IPermissionQueryRepository : IQueryRepository
{
    Task<GetPermissionQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetPermissionQueryResult>>> GetAll(GetAllPermissionsByDomainIdQuery request);
    Task<Result<IEnumerable<GetPermissionQueryResult>>> GetAll(GetAllPermissionsByDomainIdQuery request, long domainId);
}
