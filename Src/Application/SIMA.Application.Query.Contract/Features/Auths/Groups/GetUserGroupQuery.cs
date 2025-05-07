using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetUserGroupQuery : IQuery<Result<GetUserGroupQueryResult>>
{
    public long GroupId { get; set; }
    public long UserGroupId { get; set; }
}
