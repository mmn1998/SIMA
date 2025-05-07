using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.AssetAndConfigurations.Apis;

public class ApiDomainService : IApiDomainService
{
    private readonly SIMADBContext _context;

    public ApiDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ApiId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Apis.AnyAsync(x => x.Code == code);
        else result = !await _context.Apis.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}