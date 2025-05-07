using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.DraftReviewResults;

public class DeleteDraftReviewResultCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}