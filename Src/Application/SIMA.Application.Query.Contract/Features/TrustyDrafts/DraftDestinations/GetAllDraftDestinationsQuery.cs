using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.DraftDestinations
{
    public class GetAllDraftDestinationsQuery : BaseRequest, IQuery<Result<IEnumerable<GetDraftDestinationQueryResult>>>
    {
    }
}
