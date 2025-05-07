using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Contracts;

public interface IGoodsQuorumPriceDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, GoodsQuorumPriceId? id = null);
}