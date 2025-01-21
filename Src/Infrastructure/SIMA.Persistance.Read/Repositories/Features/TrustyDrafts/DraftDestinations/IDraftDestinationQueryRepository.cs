using SIMA.Application.Contract.Features.TrustyDrafts.DraftDestinations;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.DraftDestinations
{
    public interface IDraftDestinationQueryRepository : IQueryRepository
    {
        Task<GetDraftDestinationQueryResult> GetById(GetDraftDestinationQuery request);
        Task<Result<IEnumerable<GetDraftDestinationQueryResult>>> GetAll(GetAllDraftDestinationsQuery request);
    }
}
