using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Entities;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Contracts;

public interface ITriggerStatusRepository : IRepository<TriggerStatus>
{
    Task<TriggerStatus> GetById(TriggerStatusId id);
}