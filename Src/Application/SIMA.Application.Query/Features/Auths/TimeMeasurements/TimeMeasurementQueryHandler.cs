using SIMA.Application.Query.Contract.Features.Auths.TimeMeasurements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.TimeMeasurements;

namespace SIMA.Application.Query.Features.Auths.TimeMeasurements;

public class TimeMeasurementQueryHandler : IQueryHandler<GetTimeMeasurementQuery, Result<GetTimeMeasurementQueryResult>>,
    IQueryHandler<GetAllTimeMeasurementsQuery, Result<IEnumerable<GetTimeMeasurementQueryResult>>>
{
    private readonly ITimeMeasurementQueryRepository _repository;

    public TimeMeasurementQueryHandler(ITimeMeasurementQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetTimeMeasurementQueryResult>> Handle(GetTimeMeasurementQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetTimeMeasurementQueryResult>>> Handle(GetAllTimeMeasurementsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}