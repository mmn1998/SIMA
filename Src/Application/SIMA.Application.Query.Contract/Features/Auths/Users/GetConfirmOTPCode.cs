using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users
{
    public class GetConfirmOTPCode : IQuery<Result<LoginUserQueryResult>>
    {
        public long UserId { get; set; }
        public string Code { get; set; }
    }
}
