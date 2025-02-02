using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.ValueObjects;
using SIMA.Framework.Core.Domain;
using SIMA.Framework.Core.Repository;
using System.Text.RegularExpressions;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Contracts;

public interface ICobitScenarioRepository : IRepository<CobitScenario>
{
    Task<CobitScenario> GetById(CobitScenarioId id);
}
public interface ICobitScenarioDomainService : IDomainService
{
    void ValidateAscii(string input);
}
