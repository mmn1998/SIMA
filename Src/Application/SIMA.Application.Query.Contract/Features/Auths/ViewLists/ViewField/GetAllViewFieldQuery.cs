using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.ViewLists.ViewField
{
    public class GetAllViewFieldQuery : BaseRequest, IQuery<Result<IEnumerable<GetViewFieldQueryResult>>>
    {
        public long ViewId { get; set; }
    }
}
