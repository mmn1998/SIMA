using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Interfaces;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.DMS.Documents;

public class DocumentDomainService : IDocumentDomainService
{
    private readonly SIMADBContext _context;

    public DocumentDomainService(SIMADBContext context)
    {
        _context = context;
    }

    public async Task<string> GetDocumentExtension(long documentExtensionId)
    {
        var entity = await _context.DocumentExtensions.FirstOrDefaultAsync(de => de.Id == new DocumentExtensionId(documentExtensionId));
        entity.NullCheck();
        return entity?.Name ?? "";
    }

    public async Task<bool> IsCodeUnique(string code, long id)
    {
        bool result = false;
        if (id > 0)
            result = !await _context.Documents.AnyAsync(b => b.Code == code && b.Id != new DocumentId(id));
        else
            result = !await _context.Documents.AnyAsync(b => b.Code == code);
        return result;
    }
}
