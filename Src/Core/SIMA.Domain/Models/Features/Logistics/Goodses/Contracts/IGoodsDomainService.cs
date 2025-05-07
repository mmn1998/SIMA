using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.Goodses.Contracts;

public interface IGoodsDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, GoodsId? id = null);
}