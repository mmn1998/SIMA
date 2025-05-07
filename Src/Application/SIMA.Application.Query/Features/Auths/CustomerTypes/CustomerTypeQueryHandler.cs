using SIMA.Application.Query.Contract.Features.Auths.CustomerTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.CustomerTypes;

namespace SIMA.Application.Query.Features.Auths.CustomerTypes;

public class CustomerTypeQueryHandler : IQueryHandler<GetCustomerTypeQuery, Result<GetCustomerTypeQueryResult>>,
    IQueryHandler<GetAllCustomerTypesQuery, Result<IEnumerable<GetCustomerTypeQueryResult>>>
{
    private readonly ICustomerTypeQueryRepository _repository;

    public CustomerTypeQueryHandler(ICustomerTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCustomerTypeQueryResult>> Handle(GetCustomerTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCustomerTypeQueryResult>>> Handle(GetAllCustomerTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}