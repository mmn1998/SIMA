using SIMA.Application.Query.Contract.Features.Logistics.UnitMeasurements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.UnitMeasurements;

namespace SIMA.Application.Query.Features.Logistics.UnitMeasurements;

public class UnitMeasurementQueryHandler : IQueryHandler<GetUnitMeasurementQuery, Result<GetUnitMeasurementQueryResult>>,
    IQueryHandler<GetAllUnitMeasurementQuery, Result<IEnumerable<GetUnitMeasurementQueryResult>>>
{
    private readonly IUnitMeasurementQueryRepository _repository;

    public UnitMeasurementQueryHandler(IUnitMeasurementQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetUnitMeasurementQueryResult>> Handle(GetUnitMeasurementQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetUnitMeasurementQueryResult>>> Handle(GetAllUnitMeasurementQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}