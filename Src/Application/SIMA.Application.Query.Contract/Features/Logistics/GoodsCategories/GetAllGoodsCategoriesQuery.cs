using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.GoodsCategories;

public class GetAllGoodsCategoriesQuery : BaseRequest, IQuery<Result<IEnumerable<GetGoodsCategoryQueryResult>>>
{
}