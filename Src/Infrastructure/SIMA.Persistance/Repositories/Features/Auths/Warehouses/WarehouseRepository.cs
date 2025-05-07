using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Warehouses.Contracts;
using SIMA.Domain.Models.Features.Auths.Warehouses.Entities;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.Warehouses;

public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
{
    private readonly SIMADBContext _context;

    public WarehouseRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Warehouse> GetById(WarehouseId Id)
    {
        var entity = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}