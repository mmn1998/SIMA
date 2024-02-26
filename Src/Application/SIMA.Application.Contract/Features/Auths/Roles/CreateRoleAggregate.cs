using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Roles;

public class CreateRoleAggregate : ICommand<Result<long>>
{
    public CreateRoleCommand Role { get; set; }
    public List<CreateRolePermissionCommand>? RolePermissions { get; set; }
}
