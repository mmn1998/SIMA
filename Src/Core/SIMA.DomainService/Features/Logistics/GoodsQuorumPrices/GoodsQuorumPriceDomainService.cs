using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.Logistics.GoodsQuorumPrices;

public class GoodsQuorumPriceDomainService : IGoodsQuorumPriceDomainService
{
    private readonly SIMADBContext _context;

    public GoodsQuorumPriceDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, GoodsQuorumPriceId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.GoodsQuorumPrices.AnyAsync(x => x.Code == code);
        else result = !await _context.GoodsQuorumPrices.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}