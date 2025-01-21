using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Contracts;

public interface IDraftIssueTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DraftIssueTypeId? id = null);
}