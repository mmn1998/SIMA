using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Contracts;

public interface ITrustyDraftRepository : IRepository<TrustyDraft>
{
    Task<TrustyDraft> GetById(TrustyDraftId id);
    Task<TrustyDraft> GetByDarftNumber(string draftNumber);
}
