using SIMA.Application.Query.Contract.Features.Logistics.GoodsCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsCategories;

namespace SIMA.Application.Query.Features.Logistics.GoodsCategories;

public class GoodsCategoryQueryHandler : IQueryHandler<GetGoodsCategoryQuery, Result<GetGoodsCategoryQueryResult>>,
    IQueryHandler<GetAllGoodsCategoriesQuery, Result<IEnumerable<GetGoodsCategoryQueryResult>>>,
    IQueryHandler<GetAllGoodsCategoriesByGoodsType, Result<IEnumerable<GetGoodsCategoryQueryResult>>>
{
    private readonly IGoodsCategoryQueryRepository _repository;

    public GoodsCategoryQueryHandler(IGoodsCategoryQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetGoodsCategoryQueryResult>> Handle(GetGoodsCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetGoodsCategoryQueryResult>>> Handle(GetAllGoodsCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetGoodsCategoryQueryResult>>> Handle(GetAllGoodsCategoriesByGoodsType request, CancellationToken cancellationToken)
    {
        return await _repository.GetByGoodsTypeId(request.GoodsTypeId);
    }
}