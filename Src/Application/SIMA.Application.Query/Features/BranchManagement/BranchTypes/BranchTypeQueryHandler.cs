using SIMA.Application.Query.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.BranchManagement.BranchTypes;
public class BranchTypeQueryHandler : IQueryHandler<GetAllBranchTypesQuery, Result<IEnumerable<GetBranchTypeQueryResult>>>,
    IQueryHandler<GetBranchTypeQuery, Result<GetBranchTypeQueryResult>>
{
    private readonly IBranchTypeReadRepository _repository;

    public BranchTypeQueryHandler(IBranchTypeReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetBranchTypeQueryResult>>> Handle(GetAllBranchTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetBranchTypeQueryResult>> Handle(GetBranchTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
