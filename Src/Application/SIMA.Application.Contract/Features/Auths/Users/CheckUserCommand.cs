using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Auths.Users
{
    public class CheckUserCommand : ICommand<Result<long>>
    {
        public string UserName { get; set; }
    }
}
