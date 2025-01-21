using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;

public interface IDraftOriginDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DraftOriginId? id = null);
}