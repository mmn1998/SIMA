using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupPermissionQuery : IQuery<Result<List<GetGroupPermissionQueryResult>>>
{
    public long GroupId { get; set; }
    public long FormId { get; set; }
}
