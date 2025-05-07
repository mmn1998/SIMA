using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class DeleteUserPermissionCommand : ICommand<Result<long>>
{
    public long UserId { get; set; }
    public long UserPermissionId { get; set; }
}
