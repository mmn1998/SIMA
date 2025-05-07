using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Query.Contract.Features.Auths.Users
{
    public class GetUserNameWithSSO : ICommand<Result<LoginUserQueryResult>>
    {
        public string? Tiket { get; set; }
    }
}
