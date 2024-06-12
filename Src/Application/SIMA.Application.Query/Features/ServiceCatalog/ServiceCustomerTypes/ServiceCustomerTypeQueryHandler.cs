using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCustomerTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceCustomerTypes;

namespace SIMA.Application.Query.Features.ServiceCatalog.ServiceCustomerTypes;

public class ServiceCustomerTypeQueryHandler : IQueryHandler<GetServiceCustomerTypeQuery, Result<GetServiceCustomerTypeQueryResult>>,
    IQueryHandler<GetAllServiceCustomerTypesQuery, Result<IEnumerable<GetServiceCustomerTypeQueryResult>>>
{
    private readonly IServiceCustomerTypeQueryRepository _repository;

    public ServiceCustomerTypeQueryHandler(IServiceCustomerTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetServiceCustomerTypeQueryResult>> Handle(GetServiceCustomerTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetServiceCustomerTypeQueryResult>>> Handle(GetAllServiceCustomerTypesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.GetAll(request);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
