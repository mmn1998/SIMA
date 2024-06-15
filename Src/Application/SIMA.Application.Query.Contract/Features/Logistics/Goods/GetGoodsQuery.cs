using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.Goods;

public class GetGoodsQuery : IQuery<Result<GetGoodsQueryResult>>
{
    public long Id { get; set; }
}