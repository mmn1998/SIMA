using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.GoodsQuorumPrices;

public class GoodsQuorumPriceRepository : Repository<GoodsQuorumPrice>, IGoodsQuorumPriceRepository
{
    private readonly SIMADBContext _context;

    public GoodsQuorumPriceRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<GoodsQuorumPrice> GetById(GoodsQuorumPriceId Id)
    {
        var entity = await _context.GoodsQuorumPrices.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}