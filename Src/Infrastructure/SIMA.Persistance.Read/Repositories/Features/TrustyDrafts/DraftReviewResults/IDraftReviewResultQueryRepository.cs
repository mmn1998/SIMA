using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftReviewResults;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftReviewResults;

public interface IDraftReviewResultQueryRepository : IQueryRepository
{
    Task<GetDraftReviewResultQueryResult> GetById(GetDraftReviewResultQuery request);
    Task<Result<IEnumerable<GetDraftReviewResultQueryResult>>> GetAll(GetAllDraftReviewResultsQuery request);
}