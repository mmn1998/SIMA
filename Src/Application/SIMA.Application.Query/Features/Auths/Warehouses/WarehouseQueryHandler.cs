using SIMA.Application.Query.Contract.Features.Auths.Warehouses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Warehouses;

namespace SIMA.Application.Query.Features.Auths.Warehouses;

public class WarehouseQueryHandler : IQueryHandler<GetWarehouseQuery, Result<GetWarehouseQueryResult>>,
    IQueryHandler<GetAllWarehousesQuery, Result<IEnumerable<GetWarehouseQueryResult>>>
{
    private readonly IWarehouseQueryRepository _repository;

    public WarehouseQueryHandler(IWarehouseQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetWarehouseQueryResult>> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetWarehouseQueryResult>>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}