using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Contracts;

public interface ICategoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CategoryId? id = null);
}