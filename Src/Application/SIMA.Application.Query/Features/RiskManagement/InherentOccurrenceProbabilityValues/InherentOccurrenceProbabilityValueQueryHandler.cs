using SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilityValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.InherentOccurrenceProbabilityValues;

namespace SIMA.Application.Query.Features.RiskManagement.InherentOccurrenceProbabilityValues;

public class InherentOccurrenceProbabilityValueQueryHandler : IQueryHandler<GetInherentOccurrenceProbabilityValueQuery, Result<GetInherentOccurrenceProbabilityValueQueryResult>>,
    IQueryHandler<GetAllInherentOccurrenceProbabilityValuesQuery, Result<IEnumerable<GetInherentOccurrenceProbabilityValueQueryResult>>>
{
    private readonly IInherentOccurrenceProbabilityValueQueryRepository _repository;

    public InherentOccurrenceProbabilityValueQueryHandler(IInherentOccurrenceProbabilityValueQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetInherentOccurrenceProbabilityValueQueryResult>> Handle(GetInherentOccurrenceProbabilityValueQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetInherentOccurrenceProbabilityValueQueryResult>>> Handle(GetAllInherentOccurrenceProbabilityValuesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}