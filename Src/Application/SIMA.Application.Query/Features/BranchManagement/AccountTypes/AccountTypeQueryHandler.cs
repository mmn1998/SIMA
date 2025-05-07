using SIMA.Application.Query.Contract.Features.BranchManagement.AccountTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BranchManagement.AccountTypes;

namespace SIMA.Application.Query.Features.BranchManagement.AccountTypes;

public class AccountTypeQueryHandler : IQueryHandler<GetAccountTypeQuery, Result<GetAccountTypeQueryResult>>,
    IQueryHandler<GetAllAccountTypesQuery, Result<IEnumerable<GetAccountTypeQueryResult>>>
{
    private readonly IAccountTypeQueryRepository _repository;

    public AccountTypeQueryHandler(IAccountTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetAccountTypeQueryResult>> Handle(GetAccountTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAccountTypeQueryResult>>> Handle(GetAllAccountTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}