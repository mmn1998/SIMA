using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Contracts;

public interface IReconsilationTypeRepository : IRepository<ReconsilationType>
{
    Task<ReconsilationType> GetById(ReconsilationTypeId id);
}