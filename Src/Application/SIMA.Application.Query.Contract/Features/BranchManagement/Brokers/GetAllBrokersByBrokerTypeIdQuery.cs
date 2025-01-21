using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;

public class GetAllBrokersByBrokerTypeIdQuery : IQuery<Result<IEnumerable<GetBrokerQueryResult>>>
{
    public long BrokerTypeId { get; set; }
}
