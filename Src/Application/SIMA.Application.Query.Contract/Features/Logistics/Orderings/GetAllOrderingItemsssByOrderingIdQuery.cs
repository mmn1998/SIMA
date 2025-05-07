using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.Orderings;

public class GetAllOrderingItemsssByOrderingIdQuery : IQuery<Result<IEnumerable<GetAllOrderingItemsssByOrderingIdQueryResult>>>
{
    public long OrderingId { get; set; }
}