using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class DeleteUserCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
