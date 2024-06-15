using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.GoodsCategories;

public class GoodsCategoryDomainService : IGoodsCategoryDomainService
{
    private readonly SIMADBContext _context;

    public GoodsCategoryDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, GoodsCategoryId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.GoodsCategories.AnyAsync(x => x.Code == code);
        else result = !await _context.GoodsCategories.AnyAsync(x => x.Code == code && x.Id == Id);
        return result;
    }
}