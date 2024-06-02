using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.ServiceCustomerTypes;

public class ServiceCustomerTypeDomainService : IServiceCustomerTypeDomainService
{
    private readonly SIMADBContext _context;

    public ServiceCustomerTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ServiceCustomerTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ServiceCustomerTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.ServiceCustomerTypes.AnyAsync(x => x.Code == code && x.Id == Id);
        return result;
    }
}