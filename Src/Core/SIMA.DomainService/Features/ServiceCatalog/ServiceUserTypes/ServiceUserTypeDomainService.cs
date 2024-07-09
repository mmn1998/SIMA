using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.ServiceUserTypes;

public class ServiceUserTypeDomainService : IServiceUserTypeDomainService
{
    private readonly SIMADBContext _context;

    public ServiceUserTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ServiceUserTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ServiceUserTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ServiceUserTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}