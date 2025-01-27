using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Contracts;

public interface IAffectedHistoryRepository : IRepository<AffectedHistory>
{
    Task<AffectedHistory> GetById(AffectedHistoryId id);
}