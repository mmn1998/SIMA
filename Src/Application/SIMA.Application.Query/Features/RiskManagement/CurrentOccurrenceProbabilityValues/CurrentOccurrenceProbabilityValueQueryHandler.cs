using SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilityValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.CurrentOccurrenceProbabilityValues;

namespace SIMA.Application.Query.Features.RiskManagement.CurrentOccurrenceProbabilityValues;

public class CurrentOccurrenceProbabilityValueQueryHandler : IQueryHandler<GetCurrentOccurrenceProbabilityValueQuery, Result<GetCurrentOccurrenceProbabilityValueQueryResult>>,
    IQueryHandler<GetAllCurrentOccurrenceProbabilityValuesQuery, Result<IEnumerable<GetCurrentOccurrenceProbabilityValueQueryResult>>>
{
    private readonly ICurrentOccurrenceProbabilityValueQueryRepository _repository;

    public CurrentOccurrenceProbabilityValueQueryHandler(ICurrentOccurrenceProbabilityValueQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCurrentOccurrenceProbabilityValueQueryResult>> Handle(GetCurrentOccurrenceProbabilityValueQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCurrentOccurrenceProbabilityValueQueryResult>>> Handle(GetAllCurrentOccurrenceProbabilityValuesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}