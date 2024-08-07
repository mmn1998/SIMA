using SIMA.Application.Query.Contract.Features.Auths.ApiMethodActions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.ApiMethodActions;

namespace SIMA.Application.Query.Features.Auths.ApiMethodActions;

public class ApiMethodActionQueryHandler : IQueryHandler<GetAllApiMethodActionsQuery, Result<IEnumerable<GetApiMethodActionQueryResult>>>
{
    private readonly IApiMethodActionQueryRepository _repository;

    public ApiMethodActionQueryHandler(IApiMethodActionQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetApiMethodActionQueryResult>>> Handle(GetAllApiMethodActionsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll(request);
        return Result.Ok(result);
    }
}
