using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Groups;

public class DeleteGroupPermissionCommand : ICommand<Result<long>>
{
    public long GroupId { get; set; }
    public long GroupPermissionId { get; set; }
}
