using SIMA.Application.Query.Contract.Features.ServiceCatalog.OrganizationalProjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.OrganizationalProjects;

namespace SIMA.Application.Query.Features.ServiceCatalog.OrganizationalProjects;

public class OrganizationalProjectQueryHandler : IQueryHandler<GetOrganizationalProjectQuery, Result<GetOrganizationalProjectsQueryResult>>,
IQueryHandler<GetAllOrganizationalProjectsQuery, Result<IEnumerable<GetOrganizationalProjectsQueryResult>>>
{
    private readonly IOrganizationalProjectQueryRepository _repository;

    public OrganizationalProjectQueryHandler(IOrganizationalProjectQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetOrganizationalProjectsQueryResult>> Handle(GetOrganizationalProjectQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOrganizationalProjectsQueryResult>>> Handle(GetAllOrganizationalProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
