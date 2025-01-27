using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilityValues;

public class GetAllCurrentOccurrenceProbabilityValuesQuery : BaseRequest, IQuery<Result<IEnumerable<GetCurrentOccurrenceProbabilityValueQueryResult>>>
{
}