using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes
{
    public class GetBrokerTypeQuery : IQuery<Result<GetBrokerTypeQueryResult>>
    {
        public long Id { get; set; }
    }
}
