using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.FinancialSuppliers
{
    public class FinancialSupplierRepository : Repository<FinancialSupplier>, IFinancialSupplierRepository
    {
        private readonly SIMADBContext _context;

        public FinancialSupplierRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FinancialSupplier> GetById(long id)
        {
            var stronglyTypeId = new FinancialSupplierId(id);
            var entity = await _context.FinancialSuppliers.FirstOrDefaultAsync(pt => pt.Id == stronglyTypeId);
            entity.NullCheck();
            return entity;
        }
    }
}
