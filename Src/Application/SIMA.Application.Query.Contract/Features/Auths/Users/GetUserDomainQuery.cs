using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserDomainQuery : IQuery<Result<GetUserDomainQueryResult>>
{
    public long UserId { get; set; }
    public long UserDomainId { get; set; }
}

