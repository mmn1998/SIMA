using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.GoodsStatuses;

public class GetGoodsStatusQuery : IQuery<Result<GetAllGoodsStatusQueryResult>>
{
    public long Id { get; set; }
}
