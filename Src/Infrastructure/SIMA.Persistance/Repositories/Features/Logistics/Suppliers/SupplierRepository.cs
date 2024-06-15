using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Contracts;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.Suppliers;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    private readonly SIMADBContext _context;

    public SupplierRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Supplier> GetById(SupplierId Id)
    {
        var entity = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}