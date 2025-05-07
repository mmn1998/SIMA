using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Roles;

public class UpdateRoleCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? EnglishKey { get; set; }
    public long ActiveStatusId { get; set; }
    public string? Code { get; set; }
    public List<CreateRolePermissionCommand>? RolePermissions { get; set; }
    public List<CreateFormRoleCommand>? FormRoles { get; set; }
    public List<CreateRoleUserCommand>? RoleUsers { get; set; }


}
