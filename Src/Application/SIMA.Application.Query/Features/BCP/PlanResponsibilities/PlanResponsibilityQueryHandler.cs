using SIMA.Application.Query.Contract.Features.BCP.PlanResponsibilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.PlanResponsibilities;

namespace SIMA.Application.Query.Features.BCP.PlanResponsibilities;

public class PlanResponsibilityQueryHandler : IQueryHandler<GetPlanResponsibilityQuery, Result<GetPlanResponsibilityQueryResult>>,
    IQueryHandler<GetAllPlanResponsibilitiesQuery, Result<IEnumerable<GetPlanResponsibilityQueryResult>>>
{
    private readonly IPlanResponsibilityQueryRepository _repository;

    public PlanResponsibilityQueryHandler(IPlanResponsibilityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetPlanResponsibilityQueryResult>> Handle(GetPlanResponsibilityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetPlanResponsibilityQueryResult>>> Handle(GetAllPlanResponsibilitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}