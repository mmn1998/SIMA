using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Contrcts;

public interface IDraftCurrencyOriginDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DraftCurrencyOriginId? id = null);
}