using SIMA.Application.Query.Contract.Features.RiskManagement.Severities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.Severities;

namespace SIMA.Application.Query.Features.RiskManagement.Severities;

public class SeverityQueryHandler : IQueryHandler<GetSeverityQuery, Result<GetSeverityQueryResult>>,
    IQueryHandler<GetAllSeveritiesQuery, Result<IEnumerable<GetSeverityQueryResult>>>
{
    private readonly ISeverityQueryRepository _repository;

    public SeverityQueryHandler(ISeverityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetSeverityQueryResult>> Handle(GetSeverityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetSeverityQueryResult>>> Handle(GetAllSeveritiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}