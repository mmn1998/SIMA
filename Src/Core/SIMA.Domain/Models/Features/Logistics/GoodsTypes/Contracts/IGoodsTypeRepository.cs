using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;

public interface IGoodsTypeRepository : IRepository<GoodsType>
{
    Task<GoodsType> GetById(GoodsTypeId id);
}