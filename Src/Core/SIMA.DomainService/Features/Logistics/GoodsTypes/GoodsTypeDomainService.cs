using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.GoodsTypes;

public class GoodsTypeDomainService : IGoodsTypeDomainService
{
    private readonly SIMADBContext _context;

    public GoodsTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, GoodsTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.GoodsTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.GoodsTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}