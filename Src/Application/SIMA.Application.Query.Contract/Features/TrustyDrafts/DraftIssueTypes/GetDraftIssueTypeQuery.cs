using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftIssueTypes;

public class GetDraftIssueTypeQuery : IQuery<Result<GetDraftIssueTypeQueryResult>>
{
    public long Id { get; set; }
}