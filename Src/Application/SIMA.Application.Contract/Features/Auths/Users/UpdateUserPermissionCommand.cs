using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class UpdateUserPermissionCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long PermissionId { get; set; }
    public long ActiveStatusId { get; set; }

}
