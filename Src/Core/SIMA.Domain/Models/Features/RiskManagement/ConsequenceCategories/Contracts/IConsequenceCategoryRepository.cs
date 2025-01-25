using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Contracts;

public interface IConsequenceCategoryRepository : IRepository<ConsequenceCategory>
{
    Task<ConsequenceCategory> GetById(ConsequenceCategoryId id);
}