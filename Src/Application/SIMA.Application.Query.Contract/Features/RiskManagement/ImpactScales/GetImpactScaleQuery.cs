using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ImpactScales
{
    public class GetImpactScaleQuery : IQuery<Result<GetImpactScalesQueryResult>>
    {
        public long Id { get; set; }
    }
}
