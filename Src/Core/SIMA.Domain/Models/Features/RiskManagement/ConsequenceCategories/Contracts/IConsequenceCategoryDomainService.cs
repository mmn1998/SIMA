using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Contracts;

public interface IConsequenceCategoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ConsequenceCategoryId? id = null);
}