using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Contracts;

public interface IGoodsQuorumPriceRepository : IRepository<GoodsQuorumPrice>
{
    Task<GoodsQuorumPrice> GetById(GoodsQuorumPriceId id);
}