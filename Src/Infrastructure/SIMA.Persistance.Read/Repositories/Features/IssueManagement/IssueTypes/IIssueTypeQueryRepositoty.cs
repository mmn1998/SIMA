using SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueTypes;

public interface IIssueTypeQueryRepositoty : IQueryRepository
{
    Task<Result<List<GetIssueTypesQueryResult>>> GetAll(BaseRequest baseRequest);
    Task<GetIssueTypesQueryResult> GetById(long id);
}
