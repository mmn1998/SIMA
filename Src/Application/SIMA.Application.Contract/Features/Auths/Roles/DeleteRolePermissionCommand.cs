using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Roles;

public class DeleteRolePermissionCommand : ICommand<Result<long>>
{
    public long RoleId { get; set; }
    public long RolePermissionId { get; set; }
}
