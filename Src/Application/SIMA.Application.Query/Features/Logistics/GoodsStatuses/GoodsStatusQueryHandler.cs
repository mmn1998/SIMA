using SIMA.Application.Query.Contract.Features.Logistics.GoodsStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.GoodsStatues;

namespace SIMA.Application.Query.Features.Logistics.GoodsStatuses;

public class GoodsStatusQueryHandler : IQueryHandler<GetAllGoodsStatusQuery, Result<IEnumerable<GetAllGoodsStatusQueryResult>>>,
    IQueryHandler<GetGoodsStatusQuery, Result<GetAllGoodsStatusQueryResult>>

{
    private readonly IGoodsStatusQueryRepository _repository;

    public GoodsStatusQueryHandler(IGoodsStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetAllGoodsStatusQueryResult>>> Handle(GetAllGoodsStatusQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetAllGoodsStatusQueryResult>> Handle(GetGoodsStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}