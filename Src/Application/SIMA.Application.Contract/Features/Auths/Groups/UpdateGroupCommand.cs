using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Groups;

public class UpdateGroupCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long ActiveStatusId { get; set; }
    public string Code { get; set; }
    public List<CreateGroupUserCommand>? UserGroups { get; set; }
    public List<CreateGroupPermissionCommand>? GroupPermissions { get; set; }
    public List<CreateFormGroupCommand> FormGroups { get; set; }

}
