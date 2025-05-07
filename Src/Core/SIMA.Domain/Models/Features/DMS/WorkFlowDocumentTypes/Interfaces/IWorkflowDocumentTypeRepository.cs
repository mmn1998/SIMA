using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Interfaces;

public interface IWorkflowDocumentTypeRepository : IRepository<WorkflowDocumentType>
{
    Task<WorkflowDocumentType> GetById(long id);
}
