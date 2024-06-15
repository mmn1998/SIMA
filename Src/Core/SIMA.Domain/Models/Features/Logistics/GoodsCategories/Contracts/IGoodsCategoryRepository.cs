using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.GoodsCategories.Contracts;

public interface IGoodsCategoryRepository : IRepository<GoodsCategory>
{
    Task<GoodsCategory> GetById(GoodsCategoryId id);
}