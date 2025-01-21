using SIMA.Application.Query.Contract.Features.Logistics.Orderings;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.Orderings;

namespace SIMA.Application.Query.Features.Logistics.Orderings;

public class OrderingQueryHandler :
    IQueryHandler<GetAllOrderingssByLogesticSupplyIdQuery, Result<IEnumerable<GetAllOrderingssByLogesticSupplyIdQueryResult>>>,
    IQueryHandler<GetAllOrderingItemsssByOrderingIdQuery, Result<IEnumerable<GetAllOrderingItemsssByOrderingIdQueryResult>>>
{
    private readonly IOrderingQueryRepository _repository;

    public OrderingQueryHandler(IOrderingQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetAllOrderingssByLogesticSupplyIdQueryResult>>> Handle(GetAllOrderingssByLogesticSupplyIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllByLogisticsSupplyId(logisticsSupplyId: request.LogisticsSupplyId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllOrderingItemsssByOrderingIdQueryResult>>> Handle(GetAllOrderingItemsssByOrderingIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllOrderingItemsByOrderingId(request.OrderingId);
        return Result.Ok(result);
    }
}
