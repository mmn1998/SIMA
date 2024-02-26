using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class CreateUserCommand : ICommand<Result<long>>
{

    public long? ProfileId { get; set; }

    public long? CompanyId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}
