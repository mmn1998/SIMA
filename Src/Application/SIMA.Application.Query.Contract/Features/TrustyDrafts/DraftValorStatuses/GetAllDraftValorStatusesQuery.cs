using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftValorStatuses;

public class GetAllDraftValorStatusesQuery : BaseRequest, IQuery<Result<IEnumerable<GetDraftValorStatusQueryResult>>>
{
}