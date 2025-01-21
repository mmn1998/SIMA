using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftReviewResults;

public class GetDraftReviewResultQuery : IQuery<Result<GetDraftReviewResultQueryResult>>
{
    public long Id { get; set; }
}