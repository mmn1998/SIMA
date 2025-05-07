using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.Channels;

public class ChannelDomainService : IChannelDomainService
{
    private readonly SIMADBContext _context;

    public ChannelDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<string?> GetLastCode()
    {
        var entity = await _context.Channels.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        return entity?.Code;
    }

    public async Task<bool> IsCodeUnique(string code, ChannelId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Channels.AnyAsync(x => x.Code == code);
        else result = !await _context.Channels.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}