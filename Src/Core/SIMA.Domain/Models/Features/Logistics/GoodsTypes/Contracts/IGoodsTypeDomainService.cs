using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;

public interface IGoodsTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, GoodsTypeId? id = null);
}