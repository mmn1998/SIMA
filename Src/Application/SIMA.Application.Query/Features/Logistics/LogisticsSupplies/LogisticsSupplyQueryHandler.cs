using SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsSupplies;

namespace SIMA.Application.Query.Features.Logistics.LogisticsSupplies;

public class LogisticsSupplyQueryHandler : IQueryHandler<GetAllLogisticsSuppliesQuery, Result<IEnumerable<GetLogisticsSupplyQueryResult>>>,
    IQueryHandler<GetAllMyLogisticsSuppliesQuery, Result<IEnumerable<GetLogisticsSupplyQueryResult>>>,
    IQueryHandler<GetLogisticsSupplyQuery, Result<GetLogisticsSupplyDeatilQueryResult>>,
    IQueryHandler<GetLogisticsSupplyGoodsCategoryQuery, Result<IEnumerable<GetLogisticsSupplyGoodsCategoryQueryResult>>>,
    IQueryHandler<GetAllOrderingByLogisticsSupplyIdQuery, Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>,
    IQueryHandler<GetAllPaymentCommandByLogisticsSupplyIdQuery, Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>,
    IQueryHandler<GetPrePaymentCommandByLogisticsSupplyQuery, Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>
{
    private readonly ILogisticsSupplyQueryRepository _repository;

    public LogisticsSupplyQueryHandler(ILogisticsSupplyQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetLogisticsSupplyDeatilQueryResult>> Handle(GetLogisticsSupplyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetDetail(id: request.Id, logisticsRequestId: request.LogisticsRequestId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetLogisticsSupplyQueryResult>>> Handle(GetAllLogisticsSuppliesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetLogisticsSupplyQueryResult>>> Handle(GetAllMyLogisticsSuppliesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllMy(request);
    }

    public async Task<Result<IEnumerable<GetLogisticsSupplyGoodsCategoryQueryResult>>> Handle(GetLogisticsSupplyGoodsCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetGoodsCategoryBySupplyId(request.LogisticsSupplyId);
    }

    public async Task<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>> Handle(GetAllOrderingByLogisticsSupplyIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetOrderingByLogisticsSupplyId(request.LogisticsSupplyId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>> Handle(GetAllPaymentCommandByLogisticsSupplyIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPaymentCommandByLogisticsSupplyId(request.LogisticsSupplyId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>> Handle(GetPrePaymentCommandByLogisticsSupplyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPrePaymentCommandByLogisticsSupplyId(request.LogisticsSupplyId);
        return Result.Ok(result);
    }
}
