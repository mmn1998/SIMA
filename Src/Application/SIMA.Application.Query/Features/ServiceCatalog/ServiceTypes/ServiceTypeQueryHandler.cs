using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceTypes;

namespace SIMA.Application.Query.Features.ServiceCatalog.ServiceTypes;

public class ServiceTypeQueryHandler : IQueryHandler<GetServiceTypeQuery, Result<GetServiceTypeQueryResult>>,
    IQueryHandler<GetAllServiceTypesQuery, Result<IEnumerable<GetServiceTypeQueryResult>>>
{
    private readonly IServiceTypeQueryRepository _repository;

    public ServiceTypeQueryHandler(IServiceTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetServiceTypeQueryResult>> Handle(GetServiceTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetServiceTypeQueryResult>>> Handle(GetAllServiceTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}