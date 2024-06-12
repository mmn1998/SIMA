using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskCriterias
{
    public class GetRiskCriteriaQuery : IQuery<Result<GetRiskCriteriaQueryResult>>
    {
        public long Id { get; set; }
    }
}
