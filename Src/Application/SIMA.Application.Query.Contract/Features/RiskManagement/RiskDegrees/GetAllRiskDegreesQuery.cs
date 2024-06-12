using SIMA.Application.Query.Contract.Features.RiskManagement.ThreatTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskDegrees
{
    public class GetAllRiskDegreesQuery : BaseRequest, IQuery<Result<IEnumerable<GetRiskDegreesQueryResult>>>
    {
    }
}
