using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Events
{
    public sealed record ChangeGoodsStatusEvent(List<long> logisticeRequestGoods , GoodsStatusEnum GoodsStatus) : IDomainEvent;
  
}
