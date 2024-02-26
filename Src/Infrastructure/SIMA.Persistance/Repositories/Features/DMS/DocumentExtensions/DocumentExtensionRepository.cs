using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Interfaces;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.DMS.DocumentExtensions;

public class DocumentExtensionRepository : Repository<DocumentExtension>, IDocumentExtensionRepository
{
    private readonly SIMADBContext _context;

    public DocumentExtensionRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DocumentExtension> GetById(long id)
    {
        var entity = await _context.DocumentExtensions
            .Include(de => de.WorkflowDocumentExtensions)
            .Include(de => de.Documents)
            .FirstOrDefaultAsync(de => de.Id == new DocumentExtensionId(id));
        entity.NullCheck();
        return entity;
    }
}
