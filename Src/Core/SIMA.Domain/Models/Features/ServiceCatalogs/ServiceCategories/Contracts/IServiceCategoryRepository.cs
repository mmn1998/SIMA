using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Contracts;

public interface IServiceCategoryRepository : IRepository<ServiceCategory>
{
    Task<ServiceCategory> GetById(ServiceCategoryId Id);
}