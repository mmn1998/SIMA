using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;

public class GetEvaluationCriteriaQuery : IQuery<Result<GetEvaluationCriteriaQueryResult>>
{
    public long Id { get; set; }
}