using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.Goodses.Contracts;

public interface IGoodsRepository : IRepository<Goods>
{
    Task<Goods> GetById(GoodsId id);
}