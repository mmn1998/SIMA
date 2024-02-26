using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Roles;

public class UpdateRolePermissionCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public long PermissionId { get; set; }
    public string IsActive { get; set; }
}
