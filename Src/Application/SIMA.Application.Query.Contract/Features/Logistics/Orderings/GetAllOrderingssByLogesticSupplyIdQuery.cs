using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.Orderings;

public class GetAllOrderingssByLogesticSupplyIdQuery :IQuery<Result<IEnumerable<GetAllOrderingssByLogesticSupplyIdQueryResult>>>
{
    public long LogisticsSupplyId { get; set; }
}