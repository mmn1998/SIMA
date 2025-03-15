using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Contracts;

public interface IConsequenceIntensionRepository : IRepository<ConsequenceIntension>
{
    Task<ConsequenceIntension> GetById(ConsequenceIntensionId id);
}