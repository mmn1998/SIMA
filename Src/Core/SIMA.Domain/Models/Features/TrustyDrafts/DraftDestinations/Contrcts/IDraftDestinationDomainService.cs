using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Contrcts
{
    public interface IDraftDestinationDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, DraftDestinationId? id = null);
    }
}
