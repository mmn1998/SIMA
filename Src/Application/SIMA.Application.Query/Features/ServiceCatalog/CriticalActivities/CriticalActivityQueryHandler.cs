using SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.CriticalActivities;

namespace SIMA.Application.Query.Features.ServiceCatalog.CriticalActivities;

public class CriticalActivityQueryHandler : IQueryHandler<GetCriticalActivityQuery, Result<GetCriticalActivityQueryResult>>,
    IQueryHandler<GetAllCriticalActivitiesQuery, Result<IEnumerable<GetAllCriticalActivitiesQueryResult>>>
{
    private readonly ICriticalActivitiyQueryRepository _repository;

    public CriticalActivityQueryHandler(ICriticalActivitiyQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCriticalActivityQueryResult>> Handle(GetCriticalActivityQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetDetail(id: request.Id, issueId: request.IssueId);
    }

    public async Task<Result<IEnumerable<GetAllCriticalActivitiesQueryResult>>> Handle(GetAllCriticalActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
