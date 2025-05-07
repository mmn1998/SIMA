using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.ViewLists
{
    public class GetViewListQuery : IQuery<Result<GetViewListQueryResult>>
    {
        public long Id { get; set; }
    }
}
