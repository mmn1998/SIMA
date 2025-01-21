using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.FinancialSuppliers
{
    public class FinancialSupplierDomainService : IFinancialSupplierDomainService
    {
        private readonly SIMADBContext _context;

        public FinancialSupplierDomainService(SIMADBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCodeUnique(string code, long Id)
        {
            if (Id > 0)
                return await _context.FinancialSuppliers.AnyAsync(x => x.Code == code && x.Id != new FinancialSupplierId(Id));
            else
                return await _context.FinancialSuppliers.AnyAsync(x => x.Code == code);
        }
    }
}
