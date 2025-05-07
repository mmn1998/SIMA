using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class RevokeQuery : IQuery<Result<RevokeQueryResult>>
{
    public string ExpiredAccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
