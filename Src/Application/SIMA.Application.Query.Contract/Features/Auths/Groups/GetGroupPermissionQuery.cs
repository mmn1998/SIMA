using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupPermissionQuery : IQuery<Result<GetGroupPermissionQueryResult>>
{
    public long GroupId { get; set; }
    public long GroupPermissionId { get; set; }
}
