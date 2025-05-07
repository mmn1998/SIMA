using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetProfileIdByUserIdQuery : IQuery<Result<List<GetUserByProfileIdQueryResult>>>
{
    public long UserId { get; set; }
}
