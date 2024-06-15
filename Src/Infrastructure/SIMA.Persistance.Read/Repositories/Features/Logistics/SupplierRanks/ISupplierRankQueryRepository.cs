using SIMA.Application.Query.Contract.Features.Logistics.SupplierRanks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.SupplierRanks;

public interface ISupplierRankQueryRepository : IQueryRepository
{
    Task<GetSupplierRankQueryResult> GetById(GetSupplierRankQuery request);
    Task<Result<IEnumerable<GetSupplierRankQueryResult>>> GetAll(GetAllSupplierRanksQuery request);
}