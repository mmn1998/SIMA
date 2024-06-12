using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities
{
    public class GetRiskPossibilityQuery : IQuery<Result<GetRiskPossibilitiesQueryResult>>
    {
        public long Id { get; set; }
    }
}
