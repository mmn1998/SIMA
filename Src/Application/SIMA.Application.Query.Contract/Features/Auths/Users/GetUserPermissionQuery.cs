using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserPermissionQuery : IQuery<Result<GetUserPermissionQueryResult>>
{
    public long UserId { get; set; }
    public long UserPermissionId { get; set; }
}

