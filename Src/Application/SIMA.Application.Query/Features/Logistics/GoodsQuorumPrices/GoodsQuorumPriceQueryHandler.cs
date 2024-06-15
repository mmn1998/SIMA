using SIMA.Application.Query.Contract.Features.Logistics.GoodsQuorumPrices;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsQuorumPrices;

namespace SIMA.Application.Query.Features.Logistics.GoodsTypes;

public class GoodsQuorumPriceQueryHandler : IQueryHandler<GetGoodsQuorumPriceQuery, Result<GetGoodsQuorumPriceQueryResult>>,
    IQueryHandler<GetAllGoodsQuorumPriceQuery, Result<IEnumerable<GetGoodsQuorumPriceQueryResult>>>
{
    private readonly IGoodsQuorumPriceQueryRepository _repository;

    public GoodsQuorumPriceQueryHandler(IGoodsQuorumPriceQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetGoodsQuorumPriceQueryResult>> Handle(GetGoodsQuorumPriceQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetGoodsQuorumPriceQueryResult>>> Handle(GetAllGoodsQuorumPriceQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
