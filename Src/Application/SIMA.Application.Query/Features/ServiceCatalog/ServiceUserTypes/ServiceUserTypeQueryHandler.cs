using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceUserTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceUserTypes;

namespace SIMA.Application.Query.Features.ServiceCatalog.ServiceUserTypes;

public class ServiceUserTypeQueryHandler : IQueryHandler<GetServiceUserTypeQuery, Result<GetServiceUserTypeQueryResult>>,
    IQueryHandler<GetAllServiceUserTypesQuery, Result<IEnumerable<GetServiceUserTypeQueryResult>>>
{
    private readonly IServiceUserTypeQueryRepository _repository;

    public ServiceUserTypeQueryHandler(IServiceUserTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetServiceUserTypeQueryResult>> Handle(GetServiceUserTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetServiceUserTypeQueryResult>>> Handle(GetAllServiceUserTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}