using SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueTypes;

namespace SIMA.Application.Query.Features.IssueManagement.IssueTypes;

public class IssueTypeQueryHandler : IQueryHandler<GetAllIssueTypesQuery, Result<List<GetIssueTypesQueryResult>>>,
    IQueryHandler<GetIssueTypesQuery, Result<GetIssueTypesQueryResult>>
{
    private readonly IIssueTypeQueryRepositoty _repository;

    public IssueTypeQueryHandler(IIssueTypeQueryRepositoty repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetIssueTypesQueryResult>>> Handle(GetAllIssueTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.Request);
    }

    public async Task<Result<GetIssueTypesQueryResult>> Handle(GetIssueTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
