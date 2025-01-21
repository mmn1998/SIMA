using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Contrcts;

public interface IDraftCurrencyOriginRepository : IRepository<DraftCurrencyOrigin>
{
    Task<DraftCurrencyOrigin> GetById(DraftCurrencyOriginId id);
}