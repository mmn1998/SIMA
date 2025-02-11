using SIMA.Application.Query.Contract.Features.BCP.SolutionPeriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.SolutionPeriorities;

namespace SIMA.Application.Query.Features.BCP.SolutionPeriorities;

public class SolutionPeriorityQueryHandler : IQueryHandler<GetSolutionPeriorityQuery, Result<GetSolutionPeriorityQueryResult>>,
    IQueryHandler<GetAllSolutionPerioritiesQuery, Result<IEnumerable<GetSolutionPeriorityQueryResult>>>
{
    private readonly ISolutionPeriorityQueryRepository _repository;

    public SolutionPeriorityQueryHandler(ISolutionPeriorityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetSolutionPeriorityQueryResult>> Handle(GetSolutionPeriorityQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetSolutionPeriorityQueryResult>>> Handle(GetAllSolutionPerioritiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}