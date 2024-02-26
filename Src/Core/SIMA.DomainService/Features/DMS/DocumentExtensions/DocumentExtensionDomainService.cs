using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Interfaces;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.DMS.DocumentExtensions;

public class DocumentExtensionDomainService : IDocumentExtensionDomainService
{
    private readonly SIMADBContext _context;

    public DocumentExtensionDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        bool result = false;
        if (id > 0)
            result = !await _context.DocumentExtensions.AnyAsync(b => b.Code == code && b.Id != new DocumentExtensionId(id));
        else
            result = !await _context.DocumentExtensions.AnyAsync(b => b.Code == code);
        return result;
    }
}
