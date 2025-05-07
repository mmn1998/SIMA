using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Contracts;

public interface IConsequenceLevelRepository : IRepository<ConsequenceLevel>
{
    Task<ConsequenceLevel> GetById(ConsequenceLevelId id);
}