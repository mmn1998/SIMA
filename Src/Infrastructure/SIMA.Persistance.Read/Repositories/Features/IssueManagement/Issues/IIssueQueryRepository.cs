using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues;

public interface IIssueQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetAllIssueQueryResult>>> GetAllMyIssue(GetMyIssueListQuery request);
    Task<Result<IEnumerable<GetAllIssueQueryResult>>> GetAll(GetAllIssuesQuery request);
    Task<GetIssueQueryResult> GetById(long id);
    Task<bool> IsCodeUnique(string code, long id);
    Task<IEnumerable<GetIssueHistoriesByIssueIdQueryResult>> GetIssueHistoryByIssueId(long issueId);
    Task<GetIssueHistoriesByIdQueryResult> GetIssueHistoryById(long id);
}
