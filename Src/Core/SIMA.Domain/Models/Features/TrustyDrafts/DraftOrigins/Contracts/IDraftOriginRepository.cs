using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;

public interface IDraftOriginRepository : IRepository<DraftOrigin>
{
    Task<DraftOrigin> GetById(DraftOriginId id);
    Task<DraftOrigin> GetByCode(string code);
}