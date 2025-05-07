using SIMA.Domain.Models.Features.Auths.SupplierRanks.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.SupplierRanks.Contracts;

public interface ISupplierRankDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, SupplierRankId? id = null);
    Task<bool> IsOrderingUnique(float ordering, SupplierRankId? id = null);
}
