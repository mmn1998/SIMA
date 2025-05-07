using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.ServiceCategories;

public class ServiceCategoryDomainService : IServiceCategoryDomainService
{
    private readonly SIMADBContext _context;

    public ServiceCategoryDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ServiceCategoryId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ServiceCategories.AnyAsync(x => x.Code == code);
        else result = !await _context.ServiceCategories.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}