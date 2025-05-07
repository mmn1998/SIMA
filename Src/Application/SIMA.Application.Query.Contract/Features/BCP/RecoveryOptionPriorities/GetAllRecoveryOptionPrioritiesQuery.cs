using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.RecoveryOptionPriorities;

public class GetAllRecoveryOptionPrioritiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetRecoveryOptionPriorityQueryResult>>>
{
}