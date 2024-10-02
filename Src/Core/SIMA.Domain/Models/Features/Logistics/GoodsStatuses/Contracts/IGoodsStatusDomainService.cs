using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Contracts;

public interface IGoodsStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, GoodsStatusId? id = null);
}