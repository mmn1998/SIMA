using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.ViewLists
{
    public class GetAllViewListQuery : BaseRequest, IQuery<Result<IEnumerable<GetViewListQueryResult>>>
    {
    }
}
