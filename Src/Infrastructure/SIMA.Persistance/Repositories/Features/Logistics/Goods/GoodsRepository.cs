using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.Goodses.Contracts;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.Goods;

public class GoodsRepository : Repository<SIMA.Domain.Models.Features.Logistics.Goodses.Entities.Goods>, IGoodsRepository
{
    private readonly SIMADBContext _context;

    public GoodsRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SIMA.Domain.Models.Features.Logistics.Goodses.Entities.Goods> GetById(GoodsId Id)
    {
        var entity = await _context.Goods.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}