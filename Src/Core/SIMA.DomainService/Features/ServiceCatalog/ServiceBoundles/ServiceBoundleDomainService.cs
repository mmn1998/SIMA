using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Interfaces;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.ServiceBoundles;

public class ServiceBoundleDomainService : IServiceBoundleDomainService
{
    private readonly SIMADBContext _context;

    public ServiceBoundleDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ServiceBoundleId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ServiceBoundles.AnyAsync(x => x.Code == code);
        else result = !await _context.ServiceBoundles.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}
