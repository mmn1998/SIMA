using SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.InherentOccurrenceProbabilities;

namespace SIMA.Application.Query.Features.RiskManagement.InherentOccurrenceProbabilities;

public class InherentOccurrenceProbabilityQueryHandler : IQueryHandler<GetInherentOccurrenceProbabilityQuery, Result<GetInherentOccurrenceProbabilityQueryResult>>,
    IQueryHandler<GetAllInherentOccurrenceProbabilitiesQuery, Result<IEnumerable<GetInherentOccurrenceProbabilityQueryResult>>>
{
    private readonly IInherentOccurrenceProbabilityQueryRepository _repository;

    public InherentOccurrenceProbabilityQueryHandler(IInherentOccurrenceProbabilityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetInherentOccurrenceProbabilityQueryResult>> Handle(GetInherentOccurrenceProbabilityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetInherentOccurrenceProbabilityQueryResult>>> Handle(GetAllInherentOccurrenceProbabilitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}