using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts
{
    public class GetRiskImpactQuery :IQuery<Result<GetRiskImpactsQueryResult>>
    {
        public long Id { get; set; }
    }
}
