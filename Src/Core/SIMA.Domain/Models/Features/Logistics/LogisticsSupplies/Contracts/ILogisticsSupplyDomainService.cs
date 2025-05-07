using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Contracts;

public interface ILogisticsSupplyDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, LogisticsSupplyId? id = null);
}