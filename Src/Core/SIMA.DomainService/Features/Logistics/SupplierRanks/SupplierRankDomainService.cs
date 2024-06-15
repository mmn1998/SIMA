using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.SupplierRanks;

public class SupplierRankDomainService : ISupplierRankDomainService
{
    private readonly SIMADBContext _context;

    public SupplierRankDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, SupplierRankId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.SupplierRanks.AnyAsync(x => x.Code == code);
        else result = !await _context.SupplierRanks.AnyAsync(x => x.Code == code && x.Id == Id);
        return result;
    }
}