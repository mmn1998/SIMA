using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests
{
    public class GetLogesticRequestGoodsQuery : IQuery<Result<IEnumerable<GetLogesticRequestGoodsQueryResult>>>
    {
        public long Id { get; set; }
    }
}
