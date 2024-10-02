using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Suppliers.Contracts;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.Suppliers;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    private readonly SIMADBContext _context;

    public SupplierRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Supplier> GetById(SupplierId Id)
    {
        var entity = await _context.Suppliers
            .Include(x=>x.SupplierAddressBooks)
            .Include(x=>x.SupplierAccountLists)
            .Include(x=>x.SupplierPhoneBooks)
            .FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}