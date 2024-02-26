using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Interfaces;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.DMS.DocumentTypes;

public class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
{
    private readonly SIMADBContext _context;

    public DocumentTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DocumentType> GetById(long id)
    {
        var entity = await _context.DocumentTypes
            .Include(dt => dt.Documents)
                .Include(dt => dt.WorkflowDocumentTypes)
                    .FirstOrDefaultAsync(dt => dt.Id == new DocumentTypeId(id));
        entity.NullCheck();
        return entity;
    }
}
