using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;

namespace SIMA.Application.Query.Features.Logistics.LogisticsRequests;

public class LogisticsRequestsQueryHandler : IQueryHandler<GetLogisticRequestsQuery, Result<GetLogisticRequestsQueryResult>>,
    IQueryHandler<GetLogesticRequestGoodsQuery, Result<IEnumerable<GetLogesticRequestGoodsQueryResult>>>,
    IQueryHandler<GetLogisticsRequestCodeQuery , Result<List<GetLogisticsRequestCodeQueryResult>>>,
    IQueryHandler<GetLogisticsRequestGoodsQuery, Result<IEnumerable<GetLogisticsRequestGoodsQueryResult>>>

{
    private readonly ILogisticRequestQueryRepository _repository;

    public LogisticsRequestsQueryHandler(ILogisticRequestQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetLogisticRequestsQueryResult>> Handle(GetLogisticRequestsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetLogesticRequestGoodsQueryResult>>> Handle(GetLogesticRequestGoodsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetLogesticRequestGoods(request);
        return result;
    }

    public async Task<Result<List<GetLogisticsRequestCodeQueryResult>>> Handle(GetLogisticsRequestCodeQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetLogisticsRequestCode();
    }

    public async Task<Result<IEnumerable<GetLogisticsRequestGoodsQueryResult>>> Handle(GetLogisticsRequestGoodsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetLogisticsRequestGoodsFiltered(request.LogisticsRequestId);
    }
}
