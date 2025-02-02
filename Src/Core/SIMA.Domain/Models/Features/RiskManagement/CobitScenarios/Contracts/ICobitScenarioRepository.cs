using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Contracts;

public interface ICobitScenarioRepository : IRepository<CobitScenario>
{
    Task<CobitScenario> GetById(CobitScenarioId id);
}