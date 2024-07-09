using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Contracts;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.Suppliers;

public class SupplierDomainService : ISupplierDomainService
{
    private readonly SIMADBContext _context;

    public SupplierDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, SupplierId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Suppliers.AnyAsync(x => x.Code == code);
        else result = !await _context.Suppliers.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}