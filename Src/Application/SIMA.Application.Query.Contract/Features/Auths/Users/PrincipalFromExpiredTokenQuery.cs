using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using System.Security.Claims;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class PrincipalFromExpiredTokenQuery : IQuery<Result<ClaimsPrincipal>>
{
    public string Token { get; set; }
}
