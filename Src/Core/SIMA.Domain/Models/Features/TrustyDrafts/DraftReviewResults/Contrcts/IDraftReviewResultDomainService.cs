using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Contrcts;

public interface IDraftReviewResultDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DraftReviewResultId? id = null);
}