using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.StrategyTypes;

public class GetAllStrategyTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetStrategyTypeQueryResult>>>
{
}