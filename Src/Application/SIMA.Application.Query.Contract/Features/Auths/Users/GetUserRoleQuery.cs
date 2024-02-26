using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserRoleQuery : IQuery<Result<GetUserRoleQueryResult>>
{
    public long UserId { get; set; }
    public long UserRoleId { get; set; }
}

