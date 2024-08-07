using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class UpdateUserCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long? ProfileId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public long ActiveStatusId { get; set; }
    public string? IsActive { get; set; }
    public string? SecretKey { get; set; }
    public List<CreateUserDomainCommand>? UserDomains { get; set; }
    public List<CreateUserRoleCommand>? UserRoles { get; set; }
    public List<CreateUserPermissionCommand>? UserPermissions { get; set; }
    public List<CreateUserLocationCommand>? UserLocations { get; set; }
}
