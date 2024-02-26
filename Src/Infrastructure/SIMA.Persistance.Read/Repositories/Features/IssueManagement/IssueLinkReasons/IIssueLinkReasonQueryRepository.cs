using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueLinkReasons
{
    public interface IIssueLinkReasonQueryRepository : IQueryRepository
    {
        Task<GetIssueLinkReasonQueryResult> FindById(long id);
        Task<Result<List<GetIssueLinkReasonQueryResult>>> GetAll(BaseRequest? baseRequest = null);
    }
}
