using SIMA.Application.Query.Contract.Features.TrustyDrafts.Resources;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.Resources;

namespace SIMA.Application.Query.Features.TrustyDrafts.Resources;

public class ResourceQueryHandler : IQueryHandler<GetResourceQuery, Result<GetResourceQueryResult>>,
    IQueryHandler<GetAllResourcesQuery, Result<IEnumerable<GetResourceQueryResult>>>
{
    private readonly IResourceQueryRepository _repository;

    public ResourceQueryHandler(IResourceQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetResourceQueryResult>> Handle(GetResourceQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetResourceQueryResult>>> Handle(GetAllResourcesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}