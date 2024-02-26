using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Roles;

public class CreateRolePermissionCommand : ICommand<Result<long>>
{
    public long RoleId { get; set; }
    public long PermissionId { get; set; }
}
