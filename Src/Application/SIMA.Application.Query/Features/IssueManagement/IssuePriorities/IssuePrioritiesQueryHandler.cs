using SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssuePriorities;

namespace SIMA.Application.Query.Features.IssueManagement.IssuePriorities;

public class IssuePrioritiesQueryHandler : IQueryHandler<GetAllIssuePriorotiesQuery, Result<List<GetIssuePriorotyQueryResult>>>,
    IQueryHandler<GetIssuePriorotyQuery, Result<GetIssuePriorotyQueryResult>>
{
    private readonly IIssuePriorityQueryRepository _repository;

    public IssuePrioritiesQueryHandler(IIssuePriorityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetIssuePriorotyQueryResult>>> Handle(GetAllIssuePriorotiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.Request);
    }

    public async Task<Result<GetIssuePriorotyQueryResult>> Handle(GetIssuePriorotyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
