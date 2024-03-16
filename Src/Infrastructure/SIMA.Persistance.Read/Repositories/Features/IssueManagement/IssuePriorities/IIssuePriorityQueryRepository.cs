using SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssuePriorities;

public interface IIssuePriorityQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetIssuePriorotyQueryResult>>> GetAll(GetAllIssuePriorotiesQuery request);
    Task<GetIssuePriorotyQueryResult> GetById(long id);
}
