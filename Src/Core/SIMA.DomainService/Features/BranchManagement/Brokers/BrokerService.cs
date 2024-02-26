using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.Brokers;

public class BrokerService : IBrokerService
{
    private readonly SIMADBContext _context;

    public BrokerService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        if (id > 0)
            return await _context.Brokers.AnyAsync(x => x.Code == code && x.Id != new BrokerId(id));
        else
            return await _context.Brokers.AnyAsync(x => x.Code == code);
    }
}
