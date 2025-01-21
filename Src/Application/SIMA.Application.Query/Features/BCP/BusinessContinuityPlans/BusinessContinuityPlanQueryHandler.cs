using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.BusinessContinuityPlans;

namespace SIMA.Application.Query.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanQueryHandler : IQueryHandler<GetBusinessContinuityPlanQuery, Result<GetBusinessContinuityPlanQueryResult>>,
IQueryHandler<GetAllBusinessContinuityPlansQuery, Result<IEnumerable<GetAllBusinessContinuityPlansQueryResult>>>,
IQueryHandler<GetAllPlanVersioningsByPlanIdQuery, Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>>,
IQueryHandler<GetAllPlanAssumptionsByPlanIdQuery, Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>>
{
    private readonly IBusinessContinuityPlanQueryRepository _repository;

    public BusinessContinuityPlanQueryHandler(IBusinessContinuityPlanQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBusinessContinuityPlanQueryResult>> Handle(GetBusinessContinuityPlanQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllBusinessContinuityPlansQueryResult>>> Handle(GetAllBusinessContinuityPlansQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>> Handle(GetAllPlanVersioningsByPlanIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPlanVersioningByPlanId(request.BusinessContinuityPlanId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>> Handle(GetAllPlanAssumptionsByPlanIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPlanAssumptionByPlanId(request.BusinessContinuityPlanId);
        return Result.Ok(result);
    }
}
