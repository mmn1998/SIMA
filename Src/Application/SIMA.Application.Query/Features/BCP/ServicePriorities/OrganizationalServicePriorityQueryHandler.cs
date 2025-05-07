using SIMA.Application.Query.Contract.Features.BCP.ServicePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.ServicePriorities;

namespace SIMA.Application.Query.Features.BCP.ServicePriorities;

public class OrganizationalServicePriorityQueryHandler : IQueryHandler<GetOrganizationalServicePriorityQuery, Result<GetOrganizationalServicePriorityQueryResult>>,
    IQueryHandler<GetAllOrganizationalServicePrioritiesQuery, Result<IEnumerable<GetOrganizationalServicePriorityQueryResult>>>
{
    private readonly IOrganizationalServicePriorityQueryRepository _repository;

    public OrganizationalServicePriorityQueryHandler(IOrganizationalServicePriorityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetOrganizationalServicePriorityQueryResult>> Handle(GetOrganizationalServicePriorityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOrganizationalServicePriorityQueryResult>>> Handle(GetAllOrganizationalServicePrioritiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}