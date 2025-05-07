using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftOrigins;

public class GetDraftOriginQuery : IQuery<Result<GetDraftOriginQueryResult>>
{
    public long Id { get; set; }
}