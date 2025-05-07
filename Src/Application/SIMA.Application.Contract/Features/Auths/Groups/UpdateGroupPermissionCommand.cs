using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Groups;

public class UpdateGroupPermissionCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public int GroupId { get; set; }
    public long ActiveStatusId { get; set; }
    public int PermissionId { get; set; }
}
