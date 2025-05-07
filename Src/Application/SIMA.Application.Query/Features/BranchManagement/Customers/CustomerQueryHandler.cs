using SIMA.Application.Query.Contract.Features.BranchManagement.Customers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BranchManagement.Customers;

namespace SIMA.Application.Query.Features.BranchManagement.Customers;

public class CustomerQueryHandler : IQueryHandler<GetCustomerQuery, Result<GetCustomerQueryResult>>,
    IQueryHandler<GetAllCustomersQuery, Result<IEnumerable<GetCustomerQueryResult>>>
{
    private readonly ICustomerQueryRepository _repository;

    public CustomerQueryHandler(ICustomerQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCustomerQueryResult>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCustomerQueryResult>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}