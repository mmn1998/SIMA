using SIMA.Application.Query.Contract.Features.RiskManagement.SeverityValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.SeverityValues;

namespace SIMA.Application.Query.Features.RiskManagement.SeverityValues;

public class SeverityValueQueryHandler : IQueryHandler<GetSeverityValueQuery, Result<GetSeverityValueQueryResult>>,
    IQueryHandler<GetAllSeverityValuesQuery, Result<IEnumerable<GetSeverityValueQueryResult>>>
{
    private readonly ISeverityValueQueryRepository _repository;

    public SeverityValueQueryHandler(ISeverityValueQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetSeverityValueQueryResult>> Handle(GetSeverityValueQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetSeverityValueQueryResult>>> Handle(GetAllSeverityValuesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}