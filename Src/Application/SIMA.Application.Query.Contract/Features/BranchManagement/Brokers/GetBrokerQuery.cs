using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;

public class GetBrokerQuery : IQuery<Result<GetBrokerQueryResult>>
{
    public long Id { get; set; }
}
