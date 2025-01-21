using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Contracts;

public interface IDraftTypeRepository : IRepository<DraftType>
{
    Task<DraftType> GetById(DraftTypeId id);

    Task<DraftType> GetByCode(string code);
}