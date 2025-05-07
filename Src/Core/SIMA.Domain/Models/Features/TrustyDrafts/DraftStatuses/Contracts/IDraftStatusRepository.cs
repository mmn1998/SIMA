using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Contracts;

public interface IDraftStatusRepository : IRepository<DraftStatus>
{
    Task<DraftStatus> GetById(DraftStatusId id);
    Task<DraftStatus> GetByCode(string code);
}
