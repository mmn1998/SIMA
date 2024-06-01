using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceBoundles;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceBoundles;

namespace SIMA.Application.Query.Features.ServiceCatalog.ServiceBoundles;

public class ServiceBoundleQueryHandler : IQueryHandler<GetServiceBoundleQuery, Result<GetServiceBoundleQueryResult>>,
    IQueryHandler<GetAllServiceBoundlesQuery, Result<IEnumerable<GetServiceBoundleQueryResult>>>
{
    private readonly IServiceBoundleQueryRepository _repository;

    public ServiceBoundleQueryHandler(IServiceBoundleQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetServiceBoundleQueryResult>> Handle(GetServiceBoundleQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetServiceBoundleQueryResult>>> Handle(GetAllServiceBoundlesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
