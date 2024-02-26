using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserAggregateQuery : IQuery<Result<GetUserAggregateQueryResult>>
{
    public long UserId { get; set; }
}

