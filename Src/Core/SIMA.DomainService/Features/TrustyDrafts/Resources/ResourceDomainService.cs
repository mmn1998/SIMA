using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.Resources;

public class ResourceDomainService : IResourceDomainService
{
    private readonly SIMADBContext _context;

    public ResourceDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ResourceId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.Resources.AnyAsync(x => x.Code == code);
        else result = !await _context.Resources.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}