using SIMA.Application.Query.Contract.Features.Auths.Permission;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Permissions;

public interface IPermissionQueryRepository : IQueryRepository
{
    Task<GetPermissionQueryResult> FindById(long id);
    Task<List<GetPermissionQueryResult>> GetAll();
    Task<List<GetPermissionQueryResult>> GetAll(long domainId);
}
