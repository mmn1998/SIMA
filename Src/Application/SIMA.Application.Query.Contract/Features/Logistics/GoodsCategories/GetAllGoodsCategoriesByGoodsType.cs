using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.GoodsCategories
{
    public class GetAllGoodsCategoriesByGoodsType :  IQuery<Result<IEnumerable<GetGoodsCategoryQueryResult>>>
    {
        public long GoodsTypeId { get; set; }
    }
}
