using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Logistics.LogisticsSupplies;

public class LogisticsSupplyRepository : Repository<LogisticsSupply>, ILogisticsSupplyRepository
{
    private readonly SIMADBContext _context;

    public LogisticsSupplyRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<LogisticsSupply> GetById(LogisticsSupplyId Id)
    {
        var entity = await _context.LogisticsSupplies
            .Include(x => x.LogisticsSupplyDocuments)
                .Include(x => x.LogisticsSupplyGoodses)
                    .FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
    public async Task<LogisticsSupply?> GetLastLogisticsSupply()
    {
        var entity = await _context.LogisticsSupplies.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        return entity;
    }
}