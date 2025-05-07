using SIMA.Application.Query.Contract.Features.Logistics.CandidatedSuppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.CandidatedSuppliers;

public interface ICandidatedSupplierQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetCandidatedSupplierQueryResult>>> GetAll(GetAllCandidatedSuppliersQuery request);
    Task<Result<IEnumerable<GetCandidatedSupplierQueryResult>>> GetByLogestictId(long logesticId);
    Task<Result<IEnumerable<GetCandidatedSupplierQueryResult>>> GetSelectdSupplierByLogestictId(long logesticId);
}
