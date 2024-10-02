using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRolePermissionQuery : IQuery<Result<List<GetRolePermissionQueryResult>>>
{
    public long RoleId { get; set; }
    public long FormId { get; set; }
}
