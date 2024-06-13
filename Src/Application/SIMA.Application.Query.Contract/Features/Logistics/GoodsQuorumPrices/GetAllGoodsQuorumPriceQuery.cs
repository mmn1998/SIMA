using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.GoodsQuorumPrices;

public class GetAllGoodsQuorumPriceQuery : BaseRequest, IQuery<Result<IEnumerable<GetGoodsQuorumPriceQueryResult>>>
{
}
