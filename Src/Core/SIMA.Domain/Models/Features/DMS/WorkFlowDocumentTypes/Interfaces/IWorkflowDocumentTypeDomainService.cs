using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Interfaces;

public interface IWorkflowDocumentTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
