using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Contrcts
{
    public interface IDraftDestinationRepository : IRepository<DraftDestination>
    {
        Task<DraftDestination> GetById(DraftDestinationId id);

        Task<DraftDestination> GetByCode(string code);
    }
}
