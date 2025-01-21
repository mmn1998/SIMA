using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftValorStatuses;

public class GetDraftValorStatusQuery : IQuery<Result<GetDraftValorStatusQueryResult>>
{
    public long Id { get; set; }
}