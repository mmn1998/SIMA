using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentExtensions;

public interface IWorkFlowDocumentExtensionQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetWorkFlowDocumentExtensionQueryResult>>> GetAll(GetAllWorkFlowDocumentExtensionQuery request);
    Task<GetWorkFlowDocumentExtensionQueryResult> GetById(long id);
}
