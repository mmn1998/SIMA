using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Contracts;

public interface IRecoveryPointObjectiveRepository : IRepository<RecoveryPointObjective>
{
    Task<RecoveryPointObjective> GetById(RecoveryPointObjectiveId id);
}