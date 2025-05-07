using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.ServicePriorities;

public class GetAllOrganizationalServicePrioritiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetOrganizationalServicePriorityQueryResult>>>
{
}