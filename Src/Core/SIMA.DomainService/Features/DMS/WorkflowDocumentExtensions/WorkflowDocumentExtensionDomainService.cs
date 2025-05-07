using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Interfaces;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.DMS.WorkflowDocumentExtensions;

public class WorkflowDocumentExtensionDomainService : IWorkflowDocumentExtensionDomainService
{
    private readonly SIMADBContext _context;

    public WorkflowDocumentExtensionDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long id)
    {
        throw new NotImplementedException();
    }
}
