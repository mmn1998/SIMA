using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftTypes;

public interface IDraftTypeQueryRepository : IQueryRepository
{
    Task<GetDraftTypeQueryResult> GetById(GetDraftTypeQuery request);
    Task<Result<IEnumerable<GetDraftTypeQueryResult>>> GetAll(GetAllDraftTypesQuery request);
}