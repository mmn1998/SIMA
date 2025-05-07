using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Contracts;

public interface IDraftTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DraftTypeId? id = null);
}