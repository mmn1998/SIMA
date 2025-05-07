using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.DMS.WorkFlowDocumentTypes;

public class WorkFlowDocumentTypeDomainService : IWorkflowDocumentTypeDomainService
{
    private readonly SIMADBContext _context;

    public WorkFlowDocumentTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        throw new NotImplementedException();
    }
}
