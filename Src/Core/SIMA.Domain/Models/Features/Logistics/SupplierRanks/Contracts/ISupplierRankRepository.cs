using SIMA.Domain.Models.Features.Logistics.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;

public interface ISupplierRankRepository: IRepository<SupplierRank>
{
    Task<SupplierRank> GetById(SupplierRankId id);
}