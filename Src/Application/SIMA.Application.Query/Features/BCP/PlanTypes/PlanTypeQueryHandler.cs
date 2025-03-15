using SIMA.Application.Query.Contract.Features.BCP.PlanTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.PlanTypes;

namespace SIMA.Application.Query.Features.BCP.PlanTypes;

public class PlanTypeQueryHandler : IQueryHandler<GetPlanTypeQuery, Result<GetPlanTypeQueryResult>>,
    IQueryHandler<GetAllPlanTypesQuery, Result<IEnumerable<GetPlanTypeQueryResult>>>
{
    private readonly IPlanTypeQueryRepository _repository;

    public PlanTypeQueryHandler(IPlanTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetPlanTypeQueryResult>> Handle(GetPlanTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetPlanTypeQueryResult>>> Handle(GetAllPlanTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}