using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Contracts;

public interface IServiceCategoryDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ServiceCategoryId? Id = null);
}