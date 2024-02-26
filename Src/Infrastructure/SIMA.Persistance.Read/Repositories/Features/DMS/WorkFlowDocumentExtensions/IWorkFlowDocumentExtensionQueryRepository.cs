using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentExtensions
{
    public interface IWorkFlowDocumentExtensionQueryRepository : IQueryRepository
    {
        Task<List<GetWorkFlowDocumentExtensionQueryResult>> GetAll(BaseRequest baseRequest);
        Task<GetWorkFlowDocumentExtensionQueryResult> GetById(long id);
    }
}
