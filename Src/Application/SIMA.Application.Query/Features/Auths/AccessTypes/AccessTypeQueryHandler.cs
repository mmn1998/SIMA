using SIMA.Application.Query.Contract.Features.Auths.AccessTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.AccessTypes;

namespace SIMA.Application.Query.Features.Auths.AccessTypes;

public class AccessTypeQueryHandler : IQueryHandler<GetAllAccessTypesQuery, Result<IEnumerable<GetAccessTypeQueryResult>>>,
    IQueryHandler<GetAccessTypeQuery, Result<GetAccessTypeQueryResult>>
{
    private readonly IAccessTypeQueryRepository _repository;

    public AccessTypeQueryHandler(IAccessTypeQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetAccessTypeQueryResult>> Handle(GetAccessTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAccessTypeQueryResult>>> Handle(GetAllAccessTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
