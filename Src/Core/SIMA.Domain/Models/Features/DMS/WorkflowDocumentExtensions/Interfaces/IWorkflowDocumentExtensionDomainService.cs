using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Interfaces;

public interface IWorkflowDocumentExtensionDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
