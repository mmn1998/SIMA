using SIMA.Application.Query.Contract.Features.Logistics.GoodsTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsTypes;

namespace SIMA.Application.Query.Features.Logistics.GoodsTypes;

public class GoodsTypeQueryHandler : IQueryHandler<GetGoodsTypeQuery, Result<GetGoodsTypeQueryResult>>,
    IQueryHandler<GetAllGoodsTypeQuery, Result<IEnumerable<GetGoodsTypeQueryResult>>>
{
    private readonly IGoodsTypeQueryRepository _repository;

    public GoodsTypeQueryHandler(IGoodsTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetGoodsTypeQueryResult>> Handle(GetGoodsTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetGoodsTypeQueryResult>>> Handle(GetAllGoodsTypeQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}