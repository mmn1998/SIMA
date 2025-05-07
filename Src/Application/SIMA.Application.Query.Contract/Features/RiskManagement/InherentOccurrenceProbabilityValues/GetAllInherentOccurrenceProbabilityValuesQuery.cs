using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilityValues;

public class GetAllInherentOccurrenceProbabilityValuesQuery : BaseRequest, IQuery<Result<IEnumerable<GetInherentOccurrenceProbabilityValueQueryResult>>>
{
}