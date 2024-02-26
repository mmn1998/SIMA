using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class CreateUserPermissionCommand : ICommand<Result<long>>
{
    public long UserId { get; set; }
    public long PermissionId { get; set; }
}
