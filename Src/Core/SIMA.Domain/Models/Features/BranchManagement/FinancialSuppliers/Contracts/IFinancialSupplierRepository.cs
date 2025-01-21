using SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Contracts
{
    public interface IFinancialSupplierRepository : IRepository<FinancialSupplier>
    {
        Task<FinancialSupplier> GetById(long id);
    }
}
