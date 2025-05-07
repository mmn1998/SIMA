using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public class DeleteApiAuthenticationMethodCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
