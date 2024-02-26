using SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssuePriorities;

public interface IIssuePriorityQueryRepository : IQueryRepository
{
    Task<Result<List<GetIssuePriorotyQueryResult>>> GetAll(BaseRequest baseRequest);
    Task<GetIssuePriorotyQueryResult> GetById(long id);
}
