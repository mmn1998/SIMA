using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.Services;

public class ServiceDomainService : IServiceDomainService
{
    private readonly SIMADBContext _context;

    public ServiceDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ServiceId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Services.AnyAsync(x => x.Code == code);
        else result = !await _context.Services.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}