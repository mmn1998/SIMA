using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Auths.Users
{
    public class SendOTPByUsernameCommand : IQuery<Result<LoginUserQueryResult>>
    {
        public string UserName { get; set; }
    }
}
