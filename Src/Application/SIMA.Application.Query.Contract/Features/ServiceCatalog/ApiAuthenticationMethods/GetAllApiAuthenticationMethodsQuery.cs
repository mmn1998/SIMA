using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public class GetAllApiAuthenticationMethodsQuery : BaseRequest, IQuery<Result<IEnumerable<GetApiAuthenticationMethodsQueryResult>>>
    {
    }
}
