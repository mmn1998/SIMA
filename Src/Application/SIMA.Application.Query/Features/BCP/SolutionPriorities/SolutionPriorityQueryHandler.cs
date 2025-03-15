using SIMA.Application.Query.Contract.Features.BCP.SolutionPriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.SolutionPeriorities;

namespace SIMA.Application.Query.Features.BCP.SolutionPeriorities;

public class SolutionPriorityQueryHandler : IQueryHandler<GetSolutionPriorityQuery, Result<GetSolutionPriorityQueryResult>>,
    IQueryHandler<GetAllSolutionPrioritiesQuery, Result<IEnumerable<GetSolutionPriorityQueryResult>>>
{
    private readonly ISolutionPriorityQueryRepository _repository;

    public SolutionPriorityQueryHandler(ISolutionPriorityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetSolutionPriorityQueryResult>> Handle(GetSolutionPriorityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetSolutionPriorityQueryResult>>> Handle(GetAllSolutionPrioritiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}