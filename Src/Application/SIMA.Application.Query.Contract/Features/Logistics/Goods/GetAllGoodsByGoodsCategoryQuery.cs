using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.Goods
{
    public class GetAllGoodsByGoodsCategoryQuery : IQuery<Result<IEnumerable<GetGoodsQueryResult>>>
    {
        public long GoodsCategoryId { get; set; }
    }
}
