using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class CreateFormUserCommand : ICommand<Result<long>>
{
    public long FormId { get; set; }

    //public long UserId { get; set; }
}
