using SIMA.Application.Query.Contract.Features.Logistics.Goods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.Goods;

namespace SIMA.Application.Query.Features.Logistics.Goods;

public class GoodsQueryHandler : IQueryHandler<GetGoodsQuery, Result<GetGoodsQueryResult>>,
    IQueryHandler<GetAllGoodsesQuery, Result<IEnumerable<GetGoodsQueryResult>>>,
    IQueryHandler<GetAllGoodsByGoodsCategoryQuery, Result<IEnumerable<GetGoodsQueryResult>>>
{
    private readonly IGoodsQueryRepository _repository;

    public GoodsQueryHandler(IGoodsQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetGoodsQueryResult>> Handle(GetGoodsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetGoodsQueryResult>>> Handle(GetAllGoodsesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetGoodsQueryResult>>> Handle(GetAllGoodsByGoodsCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetbyGoodsCategoryId(request.GoodsCategoryId);
    }
}