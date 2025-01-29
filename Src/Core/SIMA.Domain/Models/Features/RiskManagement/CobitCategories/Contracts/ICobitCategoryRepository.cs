using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Contracts;

public interface ICobitCategoryRepository : IRepository<CobitCategory>
{
    Task<CobitCategory> GetById(CobitCategoryId id);
}