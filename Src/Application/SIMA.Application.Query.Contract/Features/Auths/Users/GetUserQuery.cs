using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserQuery : IQuery<Result<GetUserQueryResult>>
{
    public long Id { get; set; }
}
