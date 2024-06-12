using SIMA.Application.Query.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.SubjectPriorities;

namespace SIMA.Application.Query.Features.SecurityCommitees.SubjectPriorities;

public class SubjectPriorityQueryHandler : IQueryHandler<GetSubjectPriorityQuery, Result<GetSubjectPriorityQueryResult>>,
    IQueryHandler<GetAllSubjectPrioritiesQuery, Result<IEnumerable<GetSubjectPriorityQueryResult>>>
{
    private readonly ISubjectPriorityQueryRepository _repository;

    public SubjectPriorityQueryHandler(ISubjectPriorityQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetSubjectPriorityQueryResult>>> Handle(GetAllSubjectPrioritiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetSubjectPriorityQueryResult>> Handle(GetSubjectPriorityQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        return Result.Ok(entity);
    }
}
