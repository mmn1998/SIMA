using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public class GetApiAuthenticationMethodQuery : IQuery<Result<GetApiAuthenticationMethodsQueryResult>>
    {
        public long Id { get; set; }
    }
}
