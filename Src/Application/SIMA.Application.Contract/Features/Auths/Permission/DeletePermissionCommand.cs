using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Permission;
public class DeletePermissionCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}