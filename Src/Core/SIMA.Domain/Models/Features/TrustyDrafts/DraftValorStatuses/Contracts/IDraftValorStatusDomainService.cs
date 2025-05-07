using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Contracts;

public interface IDraftValorStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DraftValorStatusId? id = null);
}