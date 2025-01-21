using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.BusinessContinuityStrategies;

namespace SIMA.Application.Query.Features.BCP.BusinessContinuityStrategies;

public class BusinessContinuityStrategyQueryHandler : IQueryHandler<GetBusinessContinuityStrategyQuery, Result<GetBusinessContinuityStrategyQueryResult>>,
    IQueryHandler<GetAllBusinessContinuityStrategiesQuery, Result<IEnumerable<GetAllBusinessContinuityStrategiesQueryResult>>>
{
    private readonly IBusinessContinuityStrategyQueryRepository _repository;

    public BusinessContinuityStrategyQueryHandler(IBusinessContinuityStrategyQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBusinessContinuityStrategyQueryResult>> Handle(GetBusinessContinuityStrategyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllBusinessContinuityStrategiesQueryResult>>> Handle(GetAllBusinessContinuityStrategiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}