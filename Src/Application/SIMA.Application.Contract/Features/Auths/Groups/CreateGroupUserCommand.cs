using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Groups;

public class CreateGroupUserCommand : ICommand<Result<long>>
{
    public long UserId { get; set; }
    //public long GroupId { get; set; }
}
