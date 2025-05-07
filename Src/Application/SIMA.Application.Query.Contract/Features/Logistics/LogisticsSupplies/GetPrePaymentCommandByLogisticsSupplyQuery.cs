using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies
{
    public class GetPrePaymentCommandByLogisticsSupplyQuery : IQuery<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>
    {
        public long LogisticsSupplyId { get; set; }
    }
}
