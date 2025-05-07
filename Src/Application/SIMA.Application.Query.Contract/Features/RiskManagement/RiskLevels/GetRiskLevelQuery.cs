using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels
{
    public class GetRiskLevelQuery : IQuery<Result<GetRiskLevelsQueryResult>>
    {
        public long Id { get; set; }
    }
}
