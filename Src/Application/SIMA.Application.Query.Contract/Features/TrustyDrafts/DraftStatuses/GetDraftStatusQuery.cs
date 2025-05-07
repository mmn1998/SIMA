using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftStatuses;

public class GetDraftStatusQuery : IQuery<Result<GetDraftStatusQueryResult>>
{
    public long Id { get; set; }
}