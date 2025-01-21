using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.GoodsStatues;

public class GoodsStatusRepository : Repository<GoodsStatus>, IGoodsStatusRepository
{
    private readonly SIMADBContext _context;

    public GoodsStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<GoodsStatus> GetById(GoodsStatusId Id)
    {
        var entity = await _context.GoodsStatus.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}