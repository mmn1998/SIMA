using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.Brokers;

public class BrokerRepository : Repository<Broker>, IBrokerRepository
{
    private readonly SIMADBContext _context;

    public BrokerRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Broker> GetById(BrokerId id)
    {
        var entity = await _context.Brokers.FirstOrDefaultAsync(b => b.Id == id);
        entity.NullCheck();
        return entity;
    }
}
