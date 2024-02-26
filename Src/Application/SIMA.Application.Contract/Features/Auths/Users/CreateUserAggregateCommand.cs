using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class CreateUserAggregateCommand : ICommand<Result<long>>
{
    public CreateUserCommand User { get; set; }
    public List<CreateUserDomainCommand>? UserDomains { get; set; }
    public List<CreateUserRoleCommand>? UserRoles { get; set; }
    public List<CreateUserPermissionCommand>? UserPermissions { get; set; }
    public List<CreateUserLocationCommand>? UserLocations { get; set; }
}

