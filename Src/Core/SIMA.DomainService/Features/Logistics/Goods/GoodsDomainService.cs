using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.Goodses.Contracts;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.Goods;

public class GoodsDomainService : IGoodsDomainService
{
    private readonly SIMADBContext _context;

    public GoodsDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, GoodsId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Goods.AnyAsync(x => x.Code == code);
        else result = !await _context.Goods.AnyAsync(x => x.Code == code && x.Id == Id);
        return result;
    }
}