using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class CreateUserDomainCommand : ICommand<Result<long>>
{
    public long DomainId { get; set; }

    public long UserId { get; set; }
}
