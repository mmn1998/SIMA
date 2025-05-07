using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Permission;

public class GetPermissionQuery : IQuery<Result<GetPermissionQueryResult>>
{
    public long Id { get; set; }
}
