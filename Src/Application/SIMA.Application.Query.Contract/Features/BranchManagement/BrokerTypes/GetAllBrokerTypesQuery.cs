using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;

public class GetAllBrokerTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetBrokerTypeQueryResult>>>
{
}
