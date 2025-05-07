using SIMA.Application.Query.Contract.Features.BCP.Origins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.Origins;

namespace SIMA.Application.Query.Features.BCP.Origins;

public class OriginQueryHandler : IQueryHandler<GetOriginQuery, Result<GetOriginQueryResult>>,
    IQueryHandler<GetAllOriginsQuery, Result<IEnumerable<GetOriginQueryResult>>>
{
    private readonly IOriginQueryRepository _repository;

    public OriginQueryHandler(IOriginQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetOriginQueryResult>> Handle(GetOriginQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOriginQueryResult>>> Handle(GetAllOriginsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}