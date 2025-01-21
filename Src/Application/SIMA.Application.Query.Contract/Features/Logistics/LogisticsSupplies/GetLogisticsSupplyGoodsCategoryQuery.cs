using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies
{
    public class GetLogisticsSupplyGoodsCategoryQuery : IQuery<Result<IEnumerable<GetLogisticsSupplyGoodsCategoryQueryResult>>> 
    {
        public long LogisticsSupplyId { get; set; }
    }
}
