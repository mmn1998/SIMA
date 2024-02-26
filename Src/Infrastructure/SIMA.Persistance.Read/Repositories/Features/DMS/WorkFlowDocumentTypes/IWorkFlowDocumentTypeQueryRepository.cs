using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentTypes;

public interface IWorkFlowDocumentTypeQueryRepository : IQueryRepository
{
    Task<Result<List<GetWorkFlowDocumentTypeQueryResult>>> GetAll(BaseRequest baseRequest);
    Task<GetWorkFlowDocumentTypeQueryResult> GetById(long id);
}
