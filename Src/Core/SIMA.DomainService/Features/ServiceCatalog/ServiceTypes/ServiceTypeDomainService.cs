using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.ServiceTypes;

public class ServiceTypeDomainService : IServiceTypeDomainService
{
    private readonly SIMADBContext _context;

    public ServiceTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ServiceTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ServiceTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ServiceTypes.AnyAsync(x => x.Code == code && x.Id == Id);
        return result;
    }
}