using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftOrigins;

public class GetAllDraftOriginsQuery : BaseRequest, IQuery<Result<IEnumerable<GetDraftOriginQueryResult>>>
{
}