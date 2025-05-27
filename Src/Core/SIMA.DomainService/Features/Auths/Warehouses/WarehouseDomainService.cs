using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Warehouses.Contracts;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Auths.Warehouses;

public class WarehouseDomainService : IWarehouseDomainService
{
    private readonly SIMADBContext _context;

    public WarehouseDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, WarehouseId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Warehouses.AnyAsync(x => x.Code == code);
        else result = !await _context.Warehouses.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}