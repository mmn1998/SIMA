using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.GoodsCategories;

public class GoodsCategoryRepository : Repository<GoodsCategory>, IGoodsCategoryRepository
{
    private readonly SIMADBContext _context;

    public GoodsCategoryRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<GoodsCategory> GetById(GoodsCategoryId Id)
    {
        var entity = await _context.GoodsCategories.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}