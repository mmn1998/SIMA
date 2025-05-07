using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftIssueTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftIssueTypes;

namespace SIMA.Application.Query.Features.TrustyDrafts.DraftIssueTypes;

public class DraftIssueTypeQueryHandler : IQueryHandler<GetDraftIssueTypeQuery, Result<GetDraftIssueTypeQueryResult>>,
    IQueryHandler<GetAllDraftIssueTypesQuery, Result<IEnumerable<GetDraftIssueTypeQueryResult>>>
{
    private readonly IDraftIssueTypeQueryRepository _repository;

    public DraftIssueTypeQueryHandler(IDraftIssueTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDraftIssueTypeQueryResult>> Handle(GetDraftIssueTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetDraftIssueTypeQueryResult>>> Handle(GetAllDraftIssueTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}