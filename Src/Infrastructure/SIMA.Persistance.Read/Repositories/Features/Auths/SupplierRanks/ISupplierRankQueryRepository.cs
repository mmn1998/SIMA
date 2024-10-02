using SIMA.Application.Query.Contract.Features.Auths.SupplierRanks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.SupplierRanks;

public interface ISupplierRankQueryRepository : IQueryRepository
{
    Task<GetSupplierRankQueryResult> GetById(GetSupplierRankQuery request);
    Task<Result<IEnumerable<GetSupplierRankQueryResult>>> GetAll(GetAllSupplierRanksQuery request);
}