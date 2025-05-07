using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ImpactScales
{
    public class GetAllImpactScalesQuery : BaseRequest, IQuery<Result<IEnumerable<GetImpactScalesQueryResult>>>
    {
    }
}
