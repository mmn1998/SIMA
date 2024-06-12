using SIMA.Application.Query.Contract.Features.RiskManagement.RiskDegrees;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures
{
    public class GetAllRiskLevelMeasuresQuery : BaseRequest, IQuery<Result<IEnumerable<GetAllRiskLevelMeasuresQueryResult>>>
    {
    }
}
