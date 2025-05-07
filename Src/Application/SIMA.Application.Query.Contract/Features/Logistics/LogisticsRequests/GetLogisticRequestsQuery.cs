using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class GetLogisticRequestsQuery : BaseRequest, IQuery<Result<GetLogisticRequestsQueryResult>>
{
    public long Id { get; set; }
}
