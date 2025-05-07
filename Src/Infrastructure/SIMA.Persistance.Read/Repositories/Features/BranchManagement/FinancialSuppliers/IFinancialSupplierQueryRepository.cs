using SIMA.Application.Query.Contract.Features.BranchManagement.FinancialSuppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.FinancialSuppliers
{
    public interface IFinancialSupplierQueryRepository : IQueryRepository
    {
        Task<GetFinancialSupplierQueryResult> GetById(long id);
        Task<Result<IEnumerable<GetFinancialSupplierQueryResult>>> GetAll(GetAllFinancialSuppliersQuery request);
    }
}
