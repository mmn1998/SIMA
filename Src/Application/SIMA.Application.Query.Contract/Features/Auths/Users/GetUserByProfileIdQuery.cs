using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserByProfileIdQuery : IQuery<Result<GetUserByProfileIdQueryResult>>
{
    public long ProfileId { get; set; }
}
