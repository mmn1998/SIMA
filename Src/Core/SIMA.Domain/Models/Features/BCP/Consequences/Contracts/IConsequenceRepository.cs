using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.Consequences.Contracts;

public interface IConsequenceRepository : IRepository<Consequence>
{
    Task<Consequence> GetById(ConsequenceId id);
}