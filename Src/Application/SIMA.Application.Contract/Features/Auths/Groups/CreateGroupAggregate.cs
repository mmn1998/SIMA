using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Groups;

public class CreateGroupAggregate : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public List<CreateGroupUserCommand>? UserGroups { get; set; }
    public List<CreateGroupPermissionCommand>? GroupPermissions { get; set; }
}
