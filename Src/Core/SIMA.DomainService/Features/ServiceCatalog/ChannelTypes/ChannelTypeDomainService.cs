using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.ServiceBoundles;

public class ChannelTypeDomainService : IChannelTypeDomainService
{
    private readonly SIMADBContext _context;

    public ChannelTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ChannelTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ChannelTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ChannelTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}