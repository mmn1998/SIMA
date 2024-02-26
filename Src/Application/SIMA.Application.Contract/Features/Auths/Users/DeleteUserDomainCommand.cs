using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class DeleteUserDomainCommand : ICommand<Result<long>>
{
    public DeleteUserDomainCommand()
    {
    }

    public long UserId { get; set; }
    public long UserDomainAccessId { get; set; }
}
