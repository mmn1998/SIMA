using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Interfaces;

public interface IWorkflowDocumentExtensionRepository : IRepository<WorkflowDocumentExtension>
{
    Task<WorkflowDocumentExtension> GetById(long id);
}
