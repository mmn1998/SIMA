using SIMA.Application.Query.Contract.Features.Auths.Groups;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Groups;

public interface IGroupQueryRepository : IQueryRepository
{
    Task<GetGroupQueryResult> FindById(long id);
    Task<Result<List<GetGroupQueryResult>>> GetAll(BaseRequest? baseRequest = null);
    Task<GetGroupPermissionQueryResult> GetGroupPermission(long groupPermissionId);
    Task<GetUserGroupQueryResult> GetGroupUser(long userGroupId);
    Task<GetGroupAggregateResult> GetGroupAggregate(long groupId);
}
