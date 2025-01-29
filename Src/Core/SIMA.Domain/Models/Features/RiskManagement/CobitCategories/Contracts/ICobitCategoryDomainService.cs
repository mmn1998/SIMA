using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Contracts;

public interface ICobitCategoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CobitCategoryId? id = null);
}