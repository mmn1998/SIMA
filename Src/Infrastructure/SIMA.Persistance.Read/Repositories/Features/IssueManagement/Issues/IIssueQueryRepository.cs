using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues
{
    public interface IIssueQueryRepository : IQueryRepository
    {
        //Task<Result<List<GetAllIssueQueryResult>>> GetAllMyIssue(BaseRequest baseRequest);
        Task<Result<List<GetAllIssueQueryResult>>> GetAllMyIssue(BaseRequest baseRequest, long? workFlowId = null);
        //Task<Result<List<GetAllIssueQueryResult>>> GetAll(BaseRequest baseRequest);
        Task<Result<List<GetAllIssueQueryResult>>> GetAll(BaseRequest baseRequest, long? workFlowId = null);
        Task<GetIssueQueryResult> GetById(long id);
        Task<bool> IsCodeUnique(string code, long id);
        Task<IEnumerable<GetIssueHistoriesByIssueIdQueryResult>> GetIssueHistoryByIssueId(long issueId);
        Task<GetIssueHistoriesByIdQueryResult> GetIssueHistoryById(long id);
    }
}
