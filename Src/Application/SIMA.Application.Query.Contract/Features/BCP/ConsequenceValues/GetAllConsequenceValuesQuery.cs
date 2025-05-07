using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.ConsequenceValues;

public class GetAllConsequenceValuesQuery : BaseRequest, IQuery<Result<IEnumerable<GetConsequenceValueQueryResult>>>
{
}