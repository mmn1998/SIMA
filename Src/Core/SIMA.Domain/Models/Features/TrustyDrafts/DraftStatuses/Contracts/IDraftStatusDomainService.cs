using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Contracts;

public interface IDraftStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DraftStatusId? id = null);
}