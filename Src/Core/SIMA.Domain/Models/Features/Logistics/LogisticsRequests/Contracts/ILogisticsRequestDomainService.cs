using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;

public interface ILogisticsRequestDomainService : IDomainService
{
    Task<bool> IsTechnological(List<long> goodsId);
    Task<bool> IsGoods(List<long> goodsId);
    Task<bool> IsHardware(List<long> goodsId);
}
