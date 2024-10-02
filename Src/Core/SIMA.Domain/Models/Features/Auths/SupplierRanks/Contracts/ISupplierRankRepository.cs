using SIMA.Domain.Models.Features.Auths.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.SupplierRanks.Contracts;

public interface ISupplierRankRepository : IRepository<SupplierRank>
{
    Task<SupplierRank> GetById(SupplierRankId id);
}