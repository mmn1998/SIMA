using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetProfileByProfileIdQuery : IQuery<Result<GetProfileByProfileIdQueryResult>>
{
    public long ProfileId { get; set; }
}
