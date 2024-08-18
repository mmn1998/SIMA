using SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Services;

namespace SIMA.Application.Query.Features.ServiceCatalog.Services;

public class ServiceQueryHandler : IQueryHandler<GetServiceQuery, Result<GetServiceQueryResult>>,
    IQueryHandler<GetAllServicesQuery, Result<IEnumerable<GetServiceQueryResult>>>
{
    private readonly IServiceQueryRepository _repository;

    public ServiceQueryHandler(IServiceQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetServiceQueryResult>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetServiceQueryResult>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
