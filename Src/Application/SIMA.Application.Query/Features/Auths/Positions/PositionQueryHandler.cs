using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Positions;

namespace SIMA.Application.Query.Features.Auths.Positions;

public class PositionQueryHandler : IQueryHandler<GetPositionQuery, Result<GetPositionQueryResult>>,
    IQueryHandler<GetAllPositionsQuery, Result<List<GetPositionQueryResult>>>
{
    private readonly IPositionQueryRepository _repository;

    public PositionQueryHandler(IPositionQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetPositionQueryResult>>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.Request);
    }

    public async Task<Result<GetPositionQueryResult>> Handle(GetPositionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);

    }
}
