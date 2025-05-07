using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Contracts;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category> GetById(CategoryId id);
}