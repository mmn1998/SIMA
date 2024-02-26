using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Roles;

public class DeleteRoleCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
