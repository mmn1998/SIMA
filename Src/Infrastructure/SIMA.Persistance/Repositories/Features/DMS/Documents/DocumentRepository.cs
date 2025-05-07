using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.Interfaces;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.DMS.Documents;

public class DocumentRepository : Repository<Document>, IDocumentRepository
{
    private readonly SIMADBContext _context;

    public DocumentRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Document> GetById(long id)
    {
        var entity = await _context.Documents.FirstOrDefaultAsync(d => d.Id == new DocumentId(id));
        entity.NullCheck();
        return entity;
    }
}
