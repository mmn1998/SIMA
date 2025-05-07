using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Contracts;

public interface IDraftValorStatusRepository : IRepository<DraftValorStatus>
{
    Task<DraftValorStatus> GetById(DraftValorStatusId id);
    Task<DraftValorStatus> GetByCode(string code);
}
