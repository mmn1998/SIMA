using SIMA.Application.Query.Contract.Features.Auths.PositionTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.PositionTypes;

namespace SIMA.Application.Query.Features.Auths.PositionTypes;

public class PositionTypeQueryHandler : IQueryHandler<GetPositionTypeQuery, Result<GetPositionTypeQueryResult>>,
    IQueryHandler<GetAllPositionTypesQuery, Result<IEnumerable<GetPositionTypeQueryResult>>>
{
    private readonly IPositionTypeQueryRepository _repository;

    public PositionTypeQueryHandler(IPositionTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetPositionTypeQueryResult>> Handle(GetPositionTypeQuery request, CancellationToken cancellationToke)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetPositionTypeQueryResult>>> Handle(GetAllPositionTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}