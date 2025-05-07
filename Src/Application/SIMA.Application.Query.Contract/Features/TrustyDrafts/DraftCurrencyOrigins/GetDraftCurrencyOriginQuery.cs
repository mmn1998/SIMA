using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftCurrencyOrigins;

public class GetDraftCurrencyOriginQuery : IQuery<Result<GetDraftCurrencyOriginQueryResult>>
{
    public long Id { get; set; }
}