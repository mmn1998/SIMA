using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftStatuses;

public interface IDraftStatusQueryRepository : IQueryRepository
{
    Task<GetDraftStatusQueryResult> GetById(GetDraftStatusQuery request);
    Task<Result<IEnumerable<GetDraftStatusQueryResult>>> GetAll(GetAllDraftStatusesQuery request);
}