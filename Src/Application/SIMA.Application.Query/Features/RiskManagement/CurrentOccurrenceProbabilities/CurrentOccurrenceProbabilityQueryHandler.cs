using SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.CurrentOccurrenceProbabilities;

namespace SIMA.Application.Query.Features.RiskManagement.CurrentOccurrenceProbabilities;

public class CurrentOccurrenceProbabilityQueryHandler : IQueryHandler<GetCurrentOccurrenceProbabilityQuery, Result<GetCurrentOccurrenceProbabilityQueryResult>>,
    IQueryHandler<GetAllCurrentOccurrenceProbabilitiesQuery, Result<IEnumerable<GetCurrentOccurrenceProbabilityQueryResult>>>
{
    private readonly ICurrentOccurrenceProbabilityQueryRepository _repository;

    public CurrentOccurrenceProbabilityQueryHandler(ICurrentOccurrenceProbabilityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCurrentOccurrenceProbabilityQueryResult>> Handle(GetCurrentOccurrenceProbabilityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCurrentOccurrenceProbabilityQueryResult>>> Handle(GetAllCurrentOccurrenceProbabilitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}