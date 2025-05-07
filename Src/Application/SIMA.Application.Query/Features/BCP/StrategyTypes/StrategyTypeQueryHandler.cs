using SIMA.Application.Query.Contract.Features.BCP.StrategyTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.StrategyTypes;

namespace SIMA.Application.Query.Features.BCP.StrategyTypes;

public class StrategyTypeQueryHandler : IQueryHandler<GetStrategyTypeQuery, Result<GetStrategyTypeQueryResult>>,
    IQueryHandler<GetAllStrategyTypesQuery, Result<IEnumerable<GetStrategyTypeQueryResult>>>
{
    private readonly IStrategyTypeQueryRepository _repository;

    public StrategyTypeQueryHandler(IStrategyTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetStrategyTypeQueryResult>> Handle(GetStrategyTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetStrategyTypeQueryResult>>> Handle(GetAllStrategyTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}