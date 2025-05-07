using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.BusinessCriticalities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.BusinessCriticalities;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.BusinessCriticalities;

public class BusinessCriticalityQueryHandler : IQueryHandler<GetBusinessCriticalityQuery, Result<GetBusinessCriticalityQueryResult>>,
    IQueryHandler<GetAllBusinessCriticalitiesQuery, Result<IEnumerable<GetBusinessCriticalityQueryResult>>>
{
    private readonly IBusinessCriticalityQueryRepository _repository;

    public BusinessCriticalityQueryHandler(IBusinessCriticalityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBusinessCriticalityQueryResult>> Handle(GetBusinessCriticalityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetBusinessCriticalityQueryResult>>> Handle(GetAllBusinessCriticalitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}