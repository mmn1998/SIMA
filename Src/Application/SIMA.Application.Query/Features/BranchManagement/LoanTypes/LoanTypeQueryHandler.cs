using SIMA.Application.Query.Contract.Features.BranchManagement.LoanTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BranchManagement.LoanTypes;

namespace SIMA.Application.Query.Features.BranchManagement.LoanTypes;

public class LoanTypeQueryHandler : IQueryHandler<GetLoanTypeQuery, Result<GetLoanTypeQueryResult>>,
    IQueryHandler<GetAllLoanTypesQuery, Result<IEnumerable<GetLoanTypeQueryResult>>>
{
    private readonly ILoanTypeQueryRepository _repository;

    public LoanTypeQueryHandler(ILoanTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetLoanTypeQueryResult>> Handle(GetLoanTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetLoanTypeQueryResult>>> Handle(GetAllLoanTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}