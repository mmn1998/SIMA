using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Interfaces;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.DMS.WorkflowDocumentExtensions;

public class WorkflowDocumentExtensionRepository : Repository<WorkflowDocumentExtension>, IWorkflowDocumentExtensionRepository
{
    private readonly SIMADBContext _context;

    public WorkflowDocumentExtensionRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<WorkflowDocumentExtension> GetById(long id)
    {
        var entity = await _context.WorkflowDocumentExtensions.FirstOrDefaultAsync(wde => wde.Id == new WorkflowDocumentExtensionId(id));
        entity.NullCheck();
        return entity;
    }
}
