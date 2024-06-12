using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures
{
    public class GetRiskLevelMeasureQuery : IQuery<Result<GetRiskLevelMeasuresQueryResult>>
    {
        public long Id { get; set; }
    }
}
