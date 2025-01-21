using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;

public class GetAllEvaluationCriteriasQuery : BaseRequest, IQuery<Result<IEnumerable<GetEvaluationCriteriaQueryResult>>>
{
}
