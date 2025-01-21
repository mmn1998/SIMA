using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftReviewResults;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftReviewResults;

namespace SIMA.Application.Query.Features.TrustyDrafts.DraftReviewResults;

public class DraftReviewResultQueryHandler : IQueryHandler<GetDraftReviewResultQuery, Result<GetDraftReviewResultQueryResult>>,
    IQueryHandler<GetAllDraftReviewResultsQuery, Result<IEnumerable<GetDraftReviewResultQueryResult>>>
{
    private readonly IDraftReviewResultQueryRepository _repository;

    public DraftReviewResultQueryHandler(IDraftReviewResultQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetDraftReviewResultQueryResult>> Handle(GetDraftReviewResultQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetDraftReviewResultQueryResult>>> Handle(GetAllDraftReviewResultsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}