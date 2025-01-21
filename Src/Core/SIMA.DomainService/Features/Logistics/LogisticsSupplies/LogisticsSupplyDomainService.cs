using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.LogisticsSupplies;

public class LogisticsSupplyDomainService : ILogisticsSupplyDomainService
{
    private readonly SIMADBContext _context;

    public LogisticsSupplyDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, LogisticsSupplyId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.LogisticsSupplies.AnyAsync(x => x.Code == code);
        else result = !await _context.LogisticsSupplies.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}