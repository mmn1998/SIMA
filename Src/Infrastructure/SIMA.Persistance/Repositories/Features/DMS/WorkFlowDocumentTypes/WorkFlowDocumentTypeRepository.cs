using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Interfaces;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.DMS.WorkFlowDocumentTypes;

public class WorkFlowDocumentTypeRepository : Repository<WorkflowDocumentType>, IWorkflowDocumentTypeRepository
{
    private readonly SIMADBContext _context;

    public WorkFlowDocumentTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<WorkflowDocumentType> GetById(long id)
    {
        var entity = await _context.WorkflowDocumentTypes.FirstOrDefaultAsync(wdt => wdt.Id == new WorkflowDocumentTypeId(id));
        entity.NullCheck();
        return entity;
    }
}
