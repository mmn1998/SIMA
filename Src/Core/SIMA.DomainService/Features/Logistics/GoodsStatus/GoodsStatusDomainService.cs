using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.GoodsStatus;

public class GoodsStatusDomainService : IGoodsStatusDomainService
{
    private readonly SIMADBContext _context;

    public GoodsStatusDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, GoodsStatusId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.GoodsStatus.AnyAsync(x => x.Code == code);
        else result = !await _context.GoodsStatus.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}