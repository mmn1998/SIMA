using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Groups;

public class CreateGroupPermissionCommand : ICommand<Result<long>>
{
    public long GroupId { get; set; }

    public long PermissionId { get; set; }
}
