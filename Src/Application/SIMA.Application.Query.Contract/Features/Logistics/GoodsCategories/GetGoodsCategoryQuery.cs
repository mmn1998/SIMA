using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.GoodsCategories;

public class GetGoodsCategoryQuery : IQuery<Result<GetGoodsCategoryQueryResult>>
{
    public long Id { get; set; }
}