using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class GetAllLogisticsRequestsQuery : BaseRequest, IQuery<Result<IEnumerable<GetLogisticRequestsQueryResult>>>
{
}