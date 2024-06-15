using SIMA.Domain.Models.Features.Logistics.SupplierRanks.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;

public interface ISupplierRankDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, SupplierRankId? id = null);
}
