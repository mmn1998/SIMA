using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;

public class GetAllBrokerQuery : BaseRequest, IQuery<Result<IEnumerable<GetBrokerQueryResult>>>
{
}
