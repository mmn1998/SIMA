using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.BranchManagement.Branches;

public class BranchQueryHandler : IQueryHandler<GetBranchQuery, Result<GetBranchQueryResult>>,
    IQueryHandler<GetAllBranchQuery, Result<IEnumerable<GetBranchQueryResult>>>,
    IQueryHandler<GetAllTrustyBranchesQuery, Result<IEnumerable<GetBranchQueryResult>>>
{
    private readonly IBranchQueryRepository _repository;

    public BranchQueryHandler(IBranchQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBranchQueryResult>> Handle(GetBranchQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetBranchQueryResult>>> Handle(GetAllBranchQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetBranchQueryResult>>> Handle(GetAllTrustyBranchesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllForTrusty(request);
    }
}
