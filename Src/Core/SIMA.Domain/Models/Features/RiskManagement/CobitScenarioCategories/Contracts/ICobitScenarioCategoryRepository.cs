using SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.Contracts;

public interface ICobitScenarioCategoryRepository : IRepository<CobitScenarioCategory>
{
    Task<CobitScenarioCategory> GetById(CobitScenarioCategoryId id);
}