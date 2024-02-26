using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Interfaces;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.DMS.DocumentTypes;

public class DocumentTypeDomainService : IDocumentTypeDomainService
{
    private readonly SIMADBContext _context;

    public DocumentTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        bool result = false;
        if (id > 0)
            result = !await _context.DocumentTypes.AnyAsync(b => b.Code == code && b.Id != new DocumentTypeId(id));
        else
            result = !await _context.DocumentTypes.AnyAsync(b => b.Code == code);
        return result;
    }
}
