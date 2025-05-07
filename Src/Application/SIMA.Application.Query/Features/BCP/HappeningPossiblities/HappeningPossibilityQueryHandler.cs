using SIMA.Application.Query.Contract.Features.BCP.HappeningPossiblities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.HappeningPossiblities;

namespace SIMA.Application.Query.Features.BCP.HappeningPossiblities;

public class HappeningPossibilityQueryHandler : IQueryHandler<GetHappeningPossibilityQuery, Result<GetHappeningPossibilityQueryResult>>,
    IQueryHandler<GetAllHappeningPossiblitiesQuery, Result<IEnumerable<GetHappeningPossibilityQueryResult>>>
{
    private readonly IHappeningPossibilityQueryRepository _repository;

    public HappeningPossibilityQueryHandler(IHappeningPossibilityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetHappeningPossibilityQueryResult>> Handle(GetHappeningPossibilityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetHappeningPossibilityQueryResult>>> Handle(GetAllHappeningPossiblitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
