using SIMA.Application.Query.Contract.Features.Auths.Roles;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Roles;

public interface IRoleQueryRepository : IQueryRepository
{
    Task<bool> IsRoleSatisfied(string code, string englishKey);
    Task<GetRoleQueryResult> FindById(long id);
    Task<Result<List<GetRoleQueryResult>>> GetAll(BaseRequest? baseRequest = null);
    Task<GetRolePermissionQueryResult> GetRolePermission(long rolePermissionId);
    Task<GetRoleAggregateResult> GetRoleAggegate(long roleId);
}
