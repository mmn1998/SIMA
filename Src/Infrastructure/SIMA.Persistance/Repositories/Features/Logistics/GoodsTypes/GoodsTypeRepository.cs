using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.GoodsTypes;

public class GoodsTypeRepository : Repository<GoodsType>, IGoodsTypeRepository
{
    private readonly SIMADBContext _context;

    public GoodsTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<GoodsType> GetById(GoodsTypeId Id)
    {
        var entity = await _context.GoodsTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}