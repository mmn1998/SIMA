using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Application.Query.Contract.Features.RiskManagement.Frequencies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.Frequencies;

namespace SIMA.Application.Query.Features.RiskManagement.Frequencies;

public class FrequencyQueryHandler :  IQueryHandler<GetAllFrequenciesQuery, Result<IEnumerable<GetFrequencyQueryResult>>>,IQueryHandler<GetFrequencyQuery, Result<GetFrequencyQueryResult>>
{
    private readonly IFrequencyQueryRepository _repository;

    public FrequencyQueryHandler(IFrequencyQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetFrequencyQueryResult>> Handle(GetFrequencyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetFrequencyQueryResult>>> Handle(GetAllFrequenciesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}