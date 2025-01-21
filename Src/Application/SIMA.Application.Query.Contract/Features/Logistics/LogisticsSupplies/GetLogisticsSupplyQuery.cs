using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;

public class GetLogisticsSupplyQuery : IQuery<Result<GetLogisticsSupplyDeatilQueryResult>>
{
    public long Id { get; set; }
    public long LogisticsRequestId { get; set; }
}
