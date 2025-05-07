using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.GoodsCategories.Contracts;

public interface IGoodsCategoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, GoodsCategoryId? id = null);
}