using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftIssueTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftIssueTypes;

public interface IDraftIssueTypeQueryRepository : IQueryRepository
{
    Task<GetDraftIssueTypeQueryResult> GetById(GetDraftIssueTypeQuery request);
    Task<Result<IEnumerable<GetDraftIssueTypeQueryResult>>> GetAll(GetAllDraftIssueTypesQuery request);
}