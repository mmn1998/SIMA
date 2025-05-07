using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.GoodsQuorumPrices;

public class GetGoodsQuorumPriceQuery : IQuery<Result<GetGoodsQuorumPriceQueryResult>>
{
    public long Id { get; set; }
}