using SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.Contracts;

public interface ICobitScenarioCategoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CobitScenarioCategoryId? id = null);
}