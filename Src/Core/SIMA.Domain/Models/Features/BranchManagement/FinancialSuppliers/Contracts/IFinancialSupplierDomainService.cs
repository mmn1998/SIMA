using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Contracts
{
    public interface IFinancialSupplierDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long Id);
    }
}
