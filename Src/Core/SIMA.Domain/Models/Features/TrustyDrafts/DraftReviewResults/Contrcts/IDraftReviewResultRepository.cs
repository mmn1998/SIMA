using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Contrcts;

public interface IDraftReviewResultRepository : IRepository<DraftReviewResult>
{
    Task<DraftReviewResult> GetById(DraftReviewResultId id);
    Task<DraftReviewResult> GetByName(string name);
}