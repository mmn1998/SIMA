using SIMA.Application.Query.Contract.Features.Auths.PositionLevels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.PositionLevels;

namespace SIMA.Application.Query.Features.Auths.PositionLevels;

public class PositionLevelQueryHandler : IQueryHandler<GetPositionLevelQuery, Result<GetPositionLevelQueryResult>>,
    IQueryHandler<GetAllPositionLevelsQuery, Result<IEnumerable<GetPositionLevelQueryResult>>>
{
    private readonly IPositionLevelQueryRepository _repository;

    public PositionLevelQueryHandler(IPositionLevelQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetPositionLevelQueryResult>> Handle(GetPositionLevelQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetPositionLevelQueryResult>>> Handle(GetAllPositionLevelsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
